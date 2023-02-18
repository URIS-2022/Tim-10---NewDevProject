using Commission.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Commission.Entities
{
    public class PresidentEntity
    {
        [Key]
        public Guid presidentId { get; set; } = Guid.NewGuid();
        public Guid personalityId { get; set; }
        [NotMapped]
        public PersonalityDto? personalityDto { get; set; }

        public override string ToString()
        {
            return "President: { PresidentId: " + this.presidentId + ", PersonalityId: " + this.personalityId + " }";
        }
    }
}
