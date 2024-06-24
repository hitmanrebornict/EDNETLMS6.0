using EDNETLMS.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class GlobalData
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly UserManager<EDNETLMSUser> _userManager;

        public GlobalData(ApplicationDbContext applicationDbContext, NotificationService notificationService, AuthenticationStateProvider authenticationStateProvider, UserManager<EDNETLMSUser> userManager)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
            _authenticationStateProvider = authenticationStateProvider;
            _userManager = userManager;
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

        public async Task<string> getUsernameString()
        {
            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            return (_userManager.GetUserName(user));
        }

		public async Task<List<string>> GetAllUsernames()
		{
			List<string> usernames = await _context.Users
												   .Select(user => user.UserName)
												   .ToListAsync();

			return usernames;
		}
	}
}