using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class Class
    {
        [Key]
        public Guid classId { get; set; }
        public string? className { get; set; }
    }
}
