using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;
using System.Diagnostics;

namespace EDNETLMS.Pages.Services
{
	public class MasterDataServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public MasterDataServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
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
	}
}