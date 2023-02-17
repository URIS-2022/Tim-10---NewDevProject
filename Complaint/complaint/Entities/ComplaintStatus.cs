using System.ComponentModel.DataAnnotations;
namespace complaint.Entities
{
    public class ComplaintStatus
    {
        [Key]
        public Guid complaintStatusId { get; set; } = Guid.NewGuid();

        [Required]
        public string statusName { get; set; }

        public override string ToString()
        {
            return "ComplaintStatus: { ComplaintStatusId: " + this.complaintStatusId + ", StatusName: " + this.statusName + " }";
        }
    }
}
