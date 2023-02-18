using System.ComponentModel.DataAnnotations;
namespace complaint.Entities
{
    public class ComplaintType
    {
        [Key]
        public Guid complaintTypeId { get; set; } = Guid.NewGuid();

        [Required]
        public string? typeName { get; set; }

        public override string ToString()
        {
            return "ComplaintType: { ComplaintTypeId: " + this.complaintTypeId + ", TypeName: " + this.typeName + " }";
        }
    }
}
