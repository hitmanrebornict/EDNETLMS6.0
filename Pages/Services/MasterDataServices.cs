using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

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

        public bool updateCountry(Country updateCountry)
        {
			try
			{
				_context.Update(updateCountry);
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

		public bool updateCourse(Course updateCourse)
		{
			try
			{
				_context.Update(updateCourse);
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
				(institution,country) => new InstitutionViewModel 
				{
				InstitutionID = institution.InstitutionID,
				InstitutionName = institution.InstitutionName,
				CountryName = country.CountryName})
				.ToListAsync();
		}

		public async Task<bool> insertIntoInstitution(Institution addedInstitution,int selectedCountryID)
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

		public bool updateInstitution(Course updateInstitution)
		{
			try
			{
				_context.Update(updateInstitution);
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
