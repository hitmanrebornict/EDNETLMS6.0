using EDNETLMS.Data;
using EDNETLMS.Models;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class DialogCreateLeadCatchUpStatusServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public DialogCreateLeadCatchUpStatusServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<bool> InsertLeadCatchUpStatusAsync(LeadCatchUpStatus LCS, int LeadID)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();

			try
			{
				LCS.LeadCatchUpStatusID = 0;
				LCS.CreatedDate = System.DateTime.Now;
				LCS.Done = false;
				LCS.LeadID = LeadID;

				await _context.LeadCatchUpStatuses.AddAsync(LCS);
				await _context.SaveChangesAsync();

				await transaction.CommitAsync();
				return true;
			}
			catch
			{
				await transaction.RollbackAsync();
				return false;
			}
		}
	}
}