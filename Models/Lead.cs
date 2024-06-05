using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EDNETLMS.Models
{
    public class Lead
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeadID { get; set; }
        public int PersonID { get; set; }
        public int CourseID { get; set; }
        public string PIC { get; set; }
        public string HowDoYouKnowUs { get; set; }
        public string Remark { get; set; }
        public string LeadStatus { get; set; }

        public Person Person { get; set; }
        public Course Course { get; set; }
        public ICollection<LeadCatchUpStatus> LeadCatchUpStatuses { get; set; }
    }
}
