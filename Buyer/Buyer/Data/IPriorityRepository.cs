using Buyer.Entities;

namespace Buyer.Data
{
    public interface IPriorityRepository
    {
        List<PriorityModel> GetPriority();
        PriorityModel GetPriorityById(Guid pid);
        PriorityModel CreatePriority(PriorityModel priority);
        PriorityModel UpdatePriority(PriorityModel priority);
        void DeletePriority(Guid pid);
        bool SaveChanges();
    }
}
