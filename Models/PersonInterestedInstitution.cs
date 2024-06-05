using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EDNETLMS.Models
{
	public class PersonInterestedInstitution
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int PIID { get; set; }

		public int PersonID { get; set; }

		public int InstitutionID { get; set; }


	}
}
