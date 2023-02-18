using System.ComponentModel.DataAnnotations;

namespace Country.Entities
{
    public class Country1
    {
       [Key]
       public Guid countryId { get; set; }
       public string? nameConuntry { get; set; }
    }
}
