using System.ComponentModel.DataAnnotations;

namespace Parcel.Entities
{
    public class CadastralMunicipality
    {
        [Key]
        public Guid cadastralMunicipalityId { get; set; }
        public string? cadastralMunicipalityName { get; set; }
    }
}
