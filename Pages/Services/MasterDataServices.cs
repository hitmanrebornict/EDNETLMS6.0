using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Diagnostics;
using static EDNETLMS.Pages.MasterDataUser;

namespace EDNETLMS.Pages.Services
{
	public class MasterDataServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;
		private readonly UserManager<EDNETLMSUser> _userManager;
		private readonly GlobalData GD;


		public MasterDataServices(ApplicationDbContext applicationDbContext, NotificationService notificationService, UserManager<EDNETLMSUser> userManager, GlobalData globalData)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
			_userManager = userManager;
			GD = globalData;

		}

		public async Task<List<Country>> readCountryListAsync()
		{
			List<Country> countryList = await _context.Countries.ToListAsync();

			return countryList;
		}

		public async Task<bool> insertIntoCountry(Country addedCountry)
		{
			addedCountry.CountryID = 0;
			try
			{
				await _context.Countries.AddAsync(addedCountry);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool deleteFromCountry(Country deleteCountry)
		{
			try
			{
				_context.Countries.Remove(deleteCountry);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> updateCountry(Country updateCountry)
		{
			try
			{
				_context.Update(updateCountry);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<Course>> readCoursesListAsync()
		{
			List<Course> courseList = await _context.Courses.ToListAsync();

			return courseList;
		}

		public async Task<bool> insertIntoCourses(Course addedCourses)
		{
			addedCourses.CourseID = 0;
			try
			{
				await _context.Courses.AddAsync(addedCourses);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool deleteFromCourse(Course deleteCourse)
		{
			try
			{
				_context.Courses.Remove(deleteCourse);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> updateCourse(Course updateCourse)
		{
			try
			{
				_context.Update(updateCourse);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<List<InstitutionViewModel>> GetInstitutionViewModelsAsync()
		{
			return await _context.Institutions
				.Join(_context.Countries,
				institution => institution.CountryID,
				country => country.CountryID,
				(institution, country) => new InstitutionViewModel
				{
					InstitutionID = institution.InstitutionID,
					InstitutionName = institution.InstitutionName,
					CountryName = country.CountryName,
					CountryID = institution.CountryID
				})
				.ToListAsync();
		}

		public async Task<bool> insertIntoInstitution(Institution addedInstitution, int selectedCountryID)
		{
			addedInstitution.InstitutionID = 0;
			addedInstitution.CountryID = selectedCountryID;
			try
			{
				await _context.Institutions.AddAsync(addedInstitution);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

		public bool deleteFromInstitution(Institution deleteInstitution)
		{
			try
			{
				_context.Institutions.Remove(deleteInstitution);
				return true;
			}
			catch
			{
				return false;
			}
		}

		public async Task<bool> updateInstitution(InstitutionViewModel selectedInstitution)
		{
			try
			{
				Institution editInstitution = new Institution
				{
					InstitutionID = selectedInstitution.InstitutionID,
					InstitutionName = selectedInstitution.InstitutionName,
					CountryID = selectedInstitution.CountryID
				};

				Debug.WriteLine(editInstitution.InstitutionID);
				Debug.WriteLine(editInstitution.InstitutionName);
				Debug.WriteLine(editInstitution.CountryID);

				_context.Institutions.Update(editInstitution);
				await _context.SaveChangesAsync();
				return true;
			}
			catch
			{
				return false;
			}
		}

        public async Task<List<UserViewModel>> getUserList()
        {
			List<UserViewModel> userList = new List<UserViewModel>();
            // var users = await UserManager.Users.ToListAsync();
            var users = _userManager.Users.ToList();

            foreach (var user in users)
            {

                var roles = await _userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    UserName = user.UserName,

                    Email = user.Email,
                    Roles = string.Join(", ", roles)
                };

                userList.Add(userViewModel);

            }

            return userList;

        }

        public async Task updateRole(string userName, string role)
        {
            // Find the user by username
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                // Handle user not found error
                GD.ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "User not found.", Duration = 4000 });
                return;
            }

            //// Find the role by name
            //var roleExists = await _roleManager.RoleExistsAsync(role);
            //if (!roleExists)
            //{
            //    // Handle role not found error
            //    GD.ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Error, Summary = "Error", Detail = "Role does not exist.", Duration = 4000 });
            //    return;
            //}

            // Remove all existing roles from the user
            var existingRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, existingRoles);

            // Add the new role to the user
            await _userManager.AddToRoleAsync(user, role);

            // Notify success
            GD.ShowNotification(new NotificationMessage { Severity = NotificationSeverity.Success, Summary = "Success", Detail = "Role updated successfully.", Duration = 4000 });
        }
    }
}