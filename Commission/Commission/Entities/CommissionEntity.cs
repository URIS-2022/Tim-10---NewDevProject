using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Commission.Entities
{
    public class CommissionEntity
    {
        [Key]
        public Guid commissionId { get; set; } 

        [Required(ErrorMessage = "Mandatory")]
        public string? nameOfCommission { get; set; }

        [ForeignKey("PresidentEntity")]
        public Guid? presidentId { get; set; }
        public PresidentEntity? president { get; set; }

        public override string ToString()
        {
            return "Commission: { CommissionId: " + this.commissionId + ", PresidentId: " + this.presidentId + " }";
        }
    }
}
