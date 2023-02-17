using Parcel.Entities;

namespace Parcel.Data
{
    public interface IDrainageRepository
    {
        List<Drainage> GetDrainageList();

        Drainage GetDrainageById(Guid drainageId);

        Drainage CreateDrainage(Drainage drainage);

        void UpdateDrainage(Drainage drainage);

        void DeleteDrainage(Guid drainageId);
        bool SaveChanges();
    }
}
