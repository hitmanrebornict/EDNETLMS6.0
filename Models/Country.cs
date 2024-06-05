using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

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
