using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDNETLMS.Models
{
	// Models/Person.cs
	public class Person
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PersonID { get; set; }

		public string FamilyName { get; set; }
		public string GivenName { get; set; }
		public DateTime DateOfBirth { get; set; }
		public string Gender { get; set; }
		public string AddressLine1 { get; set; }

		public string AddressLine2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string PostalCode { get; set; }
		public string Country { get; set; }
		public string TelNum { get; set; }
		public string EmailAddress { get; set; }
		public string EducationLevel { get; set; }
		public int? InstitutionID { get; set; }

		public string? EmergencyContactName { get; set; }
		public string? EmergencyContactPhoneNum { get; set; }
		public string? EmergencyContactEmailAddress { get; set; }
		public Institution? Institution { get; set; }
		public ICollection<Lead> Leads { get; set; }

		public ICollection<PersonInterestedCourse> PersonInterestedCourses { get; set; }
		public ICollection<PersonInterestedInstitution> PersonInterestedInstitution { get; set; }
	}
}