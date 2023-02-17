namespace Parcel.Models
{
    public class ParcelDto
    {
        public Guid parcelId { get; set; }
        public Guid userOfParcelId { get; set; }
        public int surface { get; set; }
        public string parcelNumber { get; set; }
        public Guid cadastralMunicipalityId { get; set; }
        public string immovablePropertyListNumber { get; set; }
        public Guid cultureId { get; set; }
        public Guid classId { get; set; }
        public Guid workabilityId { get; set; }
        public Guid protectedZoneId { get; set; }
        public Guid formOfPropertyId { get; set; }
        public Guid drainageId { get; set; }
        public string workabilityRealCondition { get; set; }
        public string cultureRealCondition { get; set; }
        public string classRealCondition { get; set; }
        public string protectedZoneRealCondition { get; set; }
        public string drainageRealCondition { get; set; }

    }
}
