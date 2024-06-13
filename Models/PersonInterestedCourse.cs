namespace EDNETLMS.Models
{
	public class PersonInterestedCourse
	{
		public int PersonId { get; set; }
		public Person Person { get; set; }

		public int CourseId { get; set; }
		public Course Course { get; set; }
	}
}