﻿using EDNETLMS.Data;
using Radzen;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;

namespace EDNETLMS.Pages.Services
{
	public class FollowUpLeadListPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public FollowUpLeadListPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<List<PersonLead>> readTableDataFromDatabase()
		{
			var query = from person in _context.Set<Person>()
						join lead in _context.Set<Lead>()
							on person.PersonID equals lead.LeadID
						select new { person, lead };

			var resultList = query.ToList();

            List<PersonLead> leadPersonList = new List<PersonLead>();

			leadPersonList = await _context.Leads
			.Join(_context.Persons,
				  lead => lead.PersonID,
				  person => person.PersonID,
				  (lead, person) => new {lead,person })
			.Join(_context.LeadCatchUpStatuses,
			leadCatchUpStatus => leadCatchUpStatus.lead.LeadID,
			status => status.LeadID,
			(leadCatchUpStatus,status) => new PersonLead
				  {
					  FamilyName = leadCatchUpStatus.person.FamilyName,
					  GivenName = leadCatchUpStatus.person.GivenName,
					  LeadStatus = status.LeadStatus,
					  PIC = leadCatchUpStatus.lead.PIC,
					  LeadID = leadCatchUpStatus.lead.LeadID,
					  Done = status.Done,
					  DoneDate = status.DoneDate
				  }).ToListAsync();

            return leadPersonList;
        }
	}
}