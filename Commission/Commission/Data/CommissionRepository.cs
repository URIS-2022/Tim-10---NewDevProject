using AutoMapper;
using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public class CommissionRepository : ICommissionRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public CommissionRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public CommissionDto CreateCommission(CommissionEntity commission)
        {
            commission.commissionId= Guid.NewGuid();
            var createdEntity = context.Add(commission);
            return mapper.Map<CommissionDto>(createdEntity.Entity);
        }

        public void DeleteCommission(Guid commissionId)
        {
            var commission = GetCommissionById(commissionId);
            context.Remove(commission);
        }

        public List<CommissionEntity> GetAllCommissions(Guid? presidentId = null)
        {
            return context.Commission
                .Where(r => (presidentId == null || r.presidentId == presidentId))
                .ToList();
        }

        public CommissionEntity GetCommissionById(Guid commissionId) => context.Commission.FirstOrDefault(r => r.commissionId == commissionId);

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public void UpdateCommission(CommissionEntity commission)
        {
            //Entity framework core prati entitet pa nema potrebe za implementacijom
        }
    }
}
