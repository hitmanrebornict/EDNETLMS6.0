namespace EDNETLMS.Models
{
	public class PersonInterestedInstitution
	{
		public int PersonID { get; set; }
		public Person Person { get; set; }
		public int InstitutionID { get; set; }

		public Institution Institution { get; set; }
	}
}