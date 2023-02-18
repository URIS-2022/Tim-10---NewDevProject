using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Commission.Models;

namespace Commission.Entities
{
    public class MemberEntity
    {
        [Key]
        public Guid memberId { get; set; } = Guid.NewGuid();
        public Guid personalityId { get; set; }
        [Required]
        [ForeignKey("CommissionEntity")]
        public Guid commissionId { get; set; }
        public CommissionEntity? commission { get; set; }
        [NotMapped]
        public PersonalityDto? personalityDto { get; set; }

        override
        public string ToString()
        {
            return "Member: { MemberId: " + this.memberId + ", PersonalityId: " + this.personalityId + ", CommissionId: " + this.commissionId+ ", PersonalityId: " + this.personalityId + " }";
        }
    }
}
