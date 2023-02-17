
namespace Parcel.Data
{
    public interface IParcelRepository
    {
        List<Entities.Parcel> GetParcelList();
        Entities.Parcel GetParcelById(Guid parcelId);

        Entities.Parcel CreateParcel(Entities.Parcel parcel);

        void UpdateParcel(Entities.Parcel parcel);

        void DeleteParcel(Guid parcelId);

        bool SaveChanges();
    }
}
