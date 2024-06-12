using EDNETLMS.Data;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class GlobalData
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public GlobalData(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public List<string> leadStatusList = new List<string>
			{
				"Lead",
				"Applied",
				"Converted"
			};

		public List<string> genderSelection = new List<string>
			{
				"Male",
				"Female"
			};


		public void ShowNotification(NotificationMessage message)
		{
			_notificationService.Notify(message);
		}
	}
}
