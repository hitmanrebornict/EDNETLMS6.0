using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDNETLMS.Models
{
	public class Course
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CourseID { get; set; }

		public string CourseName { get; set; }
		public string? CourseDescription { get; set; }

		public ICollection<Lead> Leads { get; set; }

		public ICollection<PersonInterestedCourse> PersonInterestedCourses { get; set; }
	}
}