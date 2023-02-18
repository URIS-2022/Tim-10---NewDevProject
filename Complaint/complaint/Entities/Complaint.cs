using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using complaint.Models;

namespace complaint.Entities
{
    public class Complaint
    {
        [Key]
        public Guid complaintId { get; set; } = Guid.NewGuid();

        [ForeignKey("ComplaintType")]
        public Guid complaintTypeId { get; set; }
        public ComplaintType complaintType { get; set; }

        [Required]
        public DateTime complaintDate { get; set; }

        [Required]
        public Guid complaintSubmitter { get; set; }

        [Required]
        public string? cause { get; set; }

        [Required]
        public string reason { get; set; }

        [Required]
        public DateTime rescriptDate { get; set; }

        [Required]
        public string rescriptNumber { get; set; }

        [ForeignKey("ComplaintStatus")]
        public Guid complaintStatusId { get; set; }
        public ComplaintStatus complaintStatus { get; set; }

        [Required]
        public string decisionNumber { get; set; }
        [Required]
        public string complaintNumber { get; set; }

        [ForeignKey("Action")]
        public Guid actionId { get; set; }

        public Action action { get; set; }


        [NotMapped]
        public BuyerDto buyer { get; set; }






    }
}
