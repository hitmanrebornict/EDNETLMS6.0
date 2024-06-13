using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDNETLMS.Models
{
	public class Institution
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int InstitutionID { get; set; }

		public string InstitutionName { get; set; }
		public int CountryID { get; set; }

		public Country Country { get; set; }
		public ICollection<Person> Persons { get; set; }
		public ICollection<PersonInterestedInstitution> PersonInterestedInstitution { get; set; }
	}

	public class InstitutionViewModel
	{
		public int InstitutionID { get; set; }
		public string InstitutionName { get; set; }
		public string CountryName { get; set; }

		public int CountryID { get; set; }
	}
}