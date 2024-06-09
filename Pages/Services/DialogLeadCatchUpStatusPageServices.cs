using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class DialogLeadCatchUpStatusPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public DialogLeadCatchUpStatusPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<List<LeadCatchUpStatus>> readLeadCatchUpStatusByLeadIDAsync(int LeadId)
		{
			return await _context.LeadCatchUpStatuses
								 .Where(p => p.LeadID == LeadId)
								 .ToListAsync();
		}

	}
}
