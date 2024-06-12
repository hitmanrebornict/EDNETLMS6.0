using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class ListTablePageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public ListTablePageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

  

		public async Task<List<PersonLead>> readTableDataFromDatabase()
		{
            List<PersonLead> leadPersonList = new List<PersonLead>();

            leadPersonList = await _context.Leads
            .Join(_context.Persons,
                  lead => lead.PersonID,
                  person => person.PersonID,
                  (lead, person) => new PersonLead
                  {
                      FamilyName = person.FamilyName,
                      GivenName = person.GivenName,
                      LeadStatus = lead.LeadStatus,
                      PIC = lead.PIC,
					  LeadID = lead.LeadID
                  }).ToListAsync();

            return leadPersonList;
        }
	}
}
