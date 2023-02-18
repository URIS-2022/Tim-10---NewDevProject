using Parcel.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parcel.Entities
{
    public class Parcel
    {
        [Key]
        public Guid parcelId { get; set; }
        public Guid userOfParcelId { get; set; }
        public int surface { get; set; }
        public string? parcelNumber { get; set; }
        public string? immovablePropertyListNumber { get; set; }
        [ForeignKey("CadastralMunicipality")]
        public Guid cadastralMunicipalityId { get; set; }
        [ForeignKey("Culture")]
        public Guid cultureId { get; set; }
        [ForeignKey("Class")]
        public Guid classId { get; set; }
        [ForeignKey("Workability")]
        public Guid workabilityId { get; set; }
        [ForeignKey("ProtectedZone")]
        public Guid protectedZoneId { get; set; }
        [ForeignKey("FormOfProperty")]
        public Guid formOfPropertyId { get; set; }
        [ForeignKey("Drainage")]
        public Guid drainageId { get; set; }
        public string? workabilityRealCondition { get; set; }
        public string? cultureRealCondition { get; set; }
        public string? classRealCondition { get; set; }
        public string? protectedZoneRealCondition { get; set; }
        public string? drainageRealCondition { get; set; }
        [NotMapped]
        public BuyerDto BuyerDto { get; set; }

        override
        public string ToString()
        {
            return "Parcel: {Id: " + this.parcelId;
        }
    }
}
