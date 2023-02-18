using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class ProtectedZone
    {
        [Key]
        public Guid protectedZoneId { get; set; }
        public string? protectedZoneName { get; set; }
    }
}
