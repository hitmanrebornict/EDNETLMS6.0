using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Reflection.Metadata.Ecma335;
using System.Diagnostics;
using Radzen;
using System;

namespace EDNETLMS.Pages.Services
{
    public class CreateLeadPageServices
    {
        private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

        public CreateLeadPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
        {
            _context = applicationDbContext;
			_notificationService = notificationService;
        }

        public async Task<List<Institution>> ReadInstitutionListAsync()
        {
            return await _context.Institutions.ToListAsync();
        }

		public async Task<List<Course>> ReadCourseListAsync()
		{
           return await _context.Courses.ToListAsync();
		}

		

		public async Task<bool> InsertDataAsync(Person person, Lead lead, List<int> selectedCoursesList, List<int> selectedInstitutionList)
		{
			using var transaction = await _context.Database.BeginTransactionAsync();
			try
			{
				// Initially set the PersonID to 0
				person.PersonID = 0;

				// Add the Person entity to the context and save changes to get the generated PersonID
				await _context.Persons.AddAsync(person);
				await _context.SaveChangesAsync();

				// Now person.PersonID contains the generated value from the database
				int generatedPersonId = person.PersonID;

				// Set the generated PersonID to the Lead entity
				lead.PersonID = generatedPersonId;
				lead.LeadStatus = "Lead";
				Debug.WriteLine(lead.LeadStatus);
				lead.LeadID = 0;

				// Add the Lead entity to the context and save changes
				await _context.Leads.AddAsync(lead);
				await _context.SaveChangesAsync();

				// Insert into PersonInterestedCourse table
				foreach (int courseId in selectedCoursesList)
				{
					var personInterestedCourse = new PersonInterestedCourse
					{
						PersonId = lead.PersonID,  // Assuming meetingAttendeeID is used here; adjust as necessary
						CourseId = courseId
					};
					_context.PersonInterestedCourses.Add(personInterestedCourse);
				}
				await _context.SaveChangesAsync();

				// Insert into PersonInterestedInstitution table
				foreach (int institutionId in selectedInstitutionList)
				{
					var personInterestedInstitution = new PersonInterestedInstitution
					{
						PersonID = lead.PersonID,  // Assuming meetingAttendeeID is used here; adjust as necessary
						InstitutionID = institutionId
					};
					_context.PersonInterestedInstitution.Add(personInterestedInstitution);
				}

				await _context.SaveChangesAsync();
				// Commit the transaction
				await transaction.CommitAsync();
				return true;
			}
			catch
			{
				// Rollback the transaction in case of an error
				await transaction.RollbackAsync();
				return false;
			}
		}

        public void ShowNotification(NotificationMessage message)
        {
            _notificationService.Notify(message);
        }

    }
}
