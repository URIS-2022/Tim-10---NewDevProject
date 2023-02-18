using System.ComponentModel.DataAnnotations.Schema;

namespace Commission.Models
{
    public class PresidentDto
    {
        public Guid presidentId { get; set; }

        
        public PersonalityDto? personality { get; set; }
    }
}
