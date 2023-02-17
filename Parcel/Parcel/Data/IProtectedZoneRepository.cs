using Parcel.Entities;

namespace Parcel.Data
{
    public interface IProtectedZoneRepository
    {
        List<ProtectedZone> GetProtectedZoneList();
        ProtectedZone GetProtectedZoneById(Guid protectedZoneId);
        ProtectedZone CreateProtectedZone(ProtectedZone protectedZone);

        void UpdateProtectedZone(ProtectedZone protectedZone);

        void DeleteProtectedZone(Guid protectedZoneId);
        bool SaveChanges();
    }
}
