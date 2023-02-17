using Parcel.Entities;
using System.Net;

namespace Parcel.Data
{
    public interface IWorkabilityRepository
    {
        List<Workability> GetWorkabilityList();
        Workability GetWorkabilityById(Guid workabilityId);
        Workability CreateWorkability(Workability workability);

        void UpdateWorkability(Workability workability);

        void DeleteWorkability(Guid workabilityId);
        bool SaveChanges();
    }
}
