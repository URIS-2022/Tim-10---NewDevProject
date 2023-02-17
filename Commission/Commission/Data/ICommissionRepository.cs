using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public interface ICommissionRepository
    {
        List<CommissionEntity> GetAllCommissions(Guid? presidentId = null);
        CommissionEntity GetCommissionById(Guid commissionId);
        CommissionDto CreateCommission(CommissionEntity commission);
        void UpdateCommission(CommissionEntity commission);
        void DeleteCommission(Guid commissionId);
        bool SaveChanges();
    }
}
