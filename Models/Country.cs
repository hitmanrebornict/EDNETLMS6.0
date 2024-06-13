using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDNETLMS.Models
{
	// Models/Country.cs
	public class Country
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CountryID { get; set; }

		public string CountryName { get; set; }

		public ICollection<Institution> Institutions { get; set; }
	}
}