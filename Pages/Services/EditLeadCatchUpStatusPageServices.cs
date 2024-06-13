using EDNETLMS.Data;
using EDNETLMS.Models;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class EditLeadCatchUpStatusPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public EditLeadCatchUpStatusPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<bool> UpdateLeadCatchUpStatusAsync(LeadCatchUpStatus leadCatchUpStatus)
		{
			try
			{
				_context.LeadCatchUpStatuses.Update(leadCatchUpStatus);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}