namespace EDNETLMS.Models
{
	public class PersonLead
	{
		public string FamilyName { get; set; }
		public string GivenName { get; set; }
		public string LeadStatus { get; set; }
		public string PIC { get; set; }

		public DateTime? DoneDate { get; set; }

		public bool Done { get; set; }

		public int LeadID { get; set; }
		
	}
}
