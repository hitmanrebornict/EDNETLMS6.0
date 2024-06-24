using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class ListTablePageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;
		private readonly UserManager<EDNETLMSUser> _userManager;

		public ListTablePageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService, UserManager<EDNETLMSUser> userManager)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
			_userManager = userManager;
		}

		public async Task<List<PersonLead>> verifyRoleAndReadData(string username)
		{
			var user = await _userManager.FindByNameAsync(username);
			var existingRoles = await _userManager.GetRolesAsync(user);

			if(existingRoles.ToString() == "Admin")
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
				  })
			.ToListAsync();

			return leadPersonList;
		}

		public async Task<List<PersonLead>> readTableDataFromDatabaseByUsername(string username)
		{
			List<PersonLead> leadPersonList = new List<PersonLead>();

			leadPersonList = await _context.Leads
				.Join(_context.Persons,
					  lead => lead.PersonID,
					  person => person.PersonID,
					  (lead, person) => new { lead, person })
				.Where(joined => joined.lead.PIC == username)
				.Select(joined => new PersonLead
				{
					FamilyName = joined.person.FamilyName,
					GivenName = joined.person.GivenName,
					LeadStatus = joined.lead.LeadStatus,
					PIC = joined.lead.PIC,
					LeadID = joined.lead.LeadID
				})
				.ToListAsync();

			return leadPersonList;
		}
	}
}