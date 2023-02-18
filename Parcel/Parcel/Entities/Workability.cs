using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class Workability
    {
        [Key]
        public Guid workabilityId { get; set; }
        public string? workabilityName { get; set; }
    }
}
