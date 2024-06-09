using EDNETLMS.Data;
using EDNETLMS.Models;
using Microsoft.EntityFrameworkCore;
using Radzen;

namespace EDNETLMS.Pages.Services
{
	public class EditLeadPageServices
	{
		private readonly ApplicationDbContext _context;
		private readonly NotificationService _notificationService;

		public EditLeadPageServices(ApplicationDbContext applicationDbContext, NotificationService notificationService)
		{
			_context = applicationDbContext;
			_notificationService = notificationService;
		}

		public async Task<Person> readPersonByIDAsync(int PersonId)
		{
			Person newPerson = new Person();

			newPerson = await _context.Persons.SingleAsync(p => p.PersonID == PersonId);

			return newPerson;
		}

		public async Task<Lead> readLeadByLeadIDAsync(string LeadString)
		{
			int LeadId; 

			Int32.TryParse(LeadString, out LeadId);

			Lead selectedLead = new Lead();

			selectedLead = await _context.Leads.SingleAsync(p => p.LeadID == LeadId);

			return selectedLead;
		}

		public async Task<List<int>> readInterestedCourseListAsync(int PersonId)
		{
			List<int> selectedInterestedCourseList = await _context.PersonInterestedCourses
															.Where(p => p.PersonId == PersonId)
															.Select(p => p.CourseId)
															.ToListAsync();

			return selectedInterestedCourseList;
		}

		public async Task<List<int>> readInterestedInstitutionListAsync(int PersonId)
		{
			List<int> selectedInterestedInstitutionList = await _context.PersonInterestedInstitution
															.Where(p => p.PersonID == PersonId)
															.Select(p => p.InstitutionID)
															.ToListAsync();

			return selectedInterestedInstitutionList;
		}

		public async Task<bool> UpdatePersonInformationAsync(Person newPerson, Lead newLead, List<int> selectedCoursesList, List<int> selectedInstitutionList)
		{
			using (var transaction = await _context.Database.BeginTransactionAsync())
			{
				try
				{
					//// Update person information
					//var existingPerson = await _context.Persons.SingleAsync(p => p.PersonID == newPerson.PersonID);
					//existingPerson = newPerson;
				
					_context.Persons.Update(newPerson);

					// Update lead information
					//var existingLead = await _context.Leads.SingleAsync(l => l.LeadID == newLead.LeadID);
					//existingLead = newLead;
				
					_context.Leads.Update(newLead);

					// Update interested courses
					var existingCourses = await _context.PersonInterestedCourses.Where(p => p.PersonId == newPerson.PersonID).ToListAsync();
					var coursesToRemove = existingCourses.Where(ec => !selectedCoursesList.Contains(ec.CourseId)).ToList();
					var coursesToAdd = selectedCoursesList.Where(sc => !existingCourses.Any(ec => ec.CourseId == sc)).Select(courseId => new PersonInterestedCourse { PersonId = newPerson.PersonID, CourseId = courseId }).ToList();

					_context.PersonInterestedCourses.RemoveRange(coursesToRemove);
					await _context.PersonInterestedCourses.AddRangeAsync(coursesToAdd);

					// Update interested institutions
					var existingInstitutions = await _context.PersonInterestedInstitution.Where(p => p.PersonID == newPerson.PersonID).ToListAsync();
					var institutionsToRemove = existingInstitutions.Where(ei => !selectedInstitutionList.Contains(ei.InstitutionID)).ToList();
					var institutionsToAdd = selectedInstitutionList.Where(si => !existingInstitutions.Any(ei => ei.InstitutionID == si)).Select(institutionId => new PersonInterestedInstitution { PersonID = newPerson.PersonID, InstitutionID = institutionId }).ToList();

					_context.PersonInterestedInstitution.RemoveRange(institutionsToRemove);
					await _context.PersonInterestedInstitution.AddRangeAsync(institutionsToAdd);

					// Save changes
					await _context.SaveChangesAsync();

					// Commit transaction
					await transaction.CommitAsync();
					return true;
				}
				catch (Exception)
				{
					// Rollback transaction if any error occurs
					await transaction.RollbackAsync();
					return false;
				}
			}
		}
	}
}
