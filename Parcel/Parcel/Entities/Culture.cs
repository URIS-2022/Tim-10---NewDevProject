using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class Culture
    {
        [Key]
        public Guid cultureId { get; set; }
        public string? cultureName { get; set; }
        public string? cultureDescription { get; set; }
    }
}
