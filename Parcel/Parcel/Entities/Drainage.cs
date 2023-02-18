using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class Drainage
    {
        [Key]
        public Guid drainageId { get; set; }
        public string? drainageName { get; set; }
    }
}
