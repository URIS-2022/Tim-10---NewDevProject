using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public interface IPresidentRepository
    {
        List<PresidentEntity> GetAllPresidents();
        PresidentEntity GetPresidentById(Guid presidentId);
        PresidentDto CreatePresident(PresidentEntity president);
        void DeletePresident(Guid presidentId);
        bool SaveChanges();
    }
}
