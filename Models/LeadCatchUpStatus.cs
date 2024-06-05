using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EDNETLMS.Models
{
    public class LeadCatchUpStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LeadCatchUpStatusID { get; set; }
        public int LeadID { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Done { get; set; }
        public DateTime? DoneDate { get; set; }
        public string LeadStatus { get; set; }
        public string LeadCatchUpRemark { get; set; }

        public Lead Lead { get; set; }
    }
}
