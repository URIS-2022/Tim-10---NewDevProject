using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class FormOfProperty
    {
        [Key]
        public Guid formOfPropertyId { get; set; }
        public string? formOfPropertyName { get; set; }
        public string? formOfPropertyDescription { get; set; }
    }
}
