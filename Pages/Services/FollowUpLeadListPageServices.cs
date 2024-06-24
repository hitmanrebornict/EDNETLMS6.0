using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class FollowUpLeadListPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;
		private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
		private readonly UserManager<EDNETLMSUser> _userManager;

		public FollowUpLeadListPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService, UserManager<EDNETLMSUser> userManager)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
			_userManager = userManager;
		}

		public async Task<List<PersonLead>> verifyRoleAndReadData(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			var existingRoles = await _userManager.GetRolesAsync(user);

			if (existingRoles.ToString() == "Admin")
			{
				return await readTableDataFromDatabase();
			}
			else
			{
				return await readTableDataFromDatabaseByUsername(username);
			}
		}
		public async Task<List<PersonLead>> readTableDataFromDatabase()
		{
				return await _context.Leads
				.Join(_context.Persons,
					  lead => lead.PersonID,
					  person => person.PersonID,
					  (lead, person) => new { lead, person })
				.Join(_context.LeadCatchUpStatuses,
				leadCatchUpStatus => leadCatchUpStatus.lead.LeadID,
				status => status.LeadID,
				(leadCatchUpStatus, status) => new PersonLead
				{
					FamilyName = leadCatchUpStatus.person.FamilyName,
					GivenName = leadCatchUpStatus.person.GivenName,
					LeadStatus = status.LeadStatus,
					PIC = leadCatchUpStatus.lead.PIC,
					LeadID = leadCatchUpStatus.lead.LeadID,
					Done = status.Done,
					DoneDate = status.DoneDate
				}).ToListAsync();
		}

		public async Task<List<PersonLead>> readTableDataFromDatabaseByUsername(string username)
		{
			return await _context.Leads
				.Join(_context.Persons,
					  lead => lead.PersonID,
					  person => person.PersonID,
					  (lead, person) => new { lead, person })
				.Join(_context.LeadCatchUpStatuses,
					  leadCatchUpStatus => leadCatchUpStatus.lead.LeadID,
					  status => status.LeadID,
					  (leadCatchUpStatus, status) => new { leadCatchUpStatus, status })
				.Where(joined => joined.leadCatchUpStatus.lead.PIC == username)
				.Select(joined => new PersonLead
				{
					FamilyName = joined.leadCatchUpStatus.person.FamilyName,
					GivenName = joined.leadCatchUpStatus.person.GivenName,
					LeadStatus = joined.status.LeadStatus,
					PIC = joined.leadCatchUpStatus.lead.PIC,
					LeadID = joined.leadCatchUpStatus.lead.LeadID,
					Done = joined.status.Done,
					DoneDate = joined.status.DoneDate
				})
				.ToListAsync();
		}
	}
}