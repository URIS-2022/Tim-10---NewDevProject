using AutoMapper;
using Buyer.Entities;

namespace Buyer.Data
{
    public class LegalEntityRepository : ILegalEntityRepository
    {

        private readonly BuyerContext context;
        private readonly IMapper mapper;

        public LegalEntityRepository(BuyerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public LegalEntity CreateLegalEntity(LegalEntity legalEntity)
        {
            legalEntity.buyerId = Guid.NewGuid();
            var newEntity = context.legalEntities.Add(legalEntity);

            return mapper.Map<LegalEntity>(newEntity.Entity);

        }

        public void DeleteLegalEntity(Guid leid)
        {
            LegalEntity legalEntity = GetLegalEntityById(leid);
            context.legalEntities.Remove(legalEntity);
        }

        public List<LegalEntity> GetLegalEntity()
        {
            return context.legalEntities.ToList();
        }

        public LegalEntity GetLegalEntityById(Guid leid)
        {
            return context.legalEntities.FirstOrDefault(le => le.buyerId == leid);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public LegalEntity UpdateLegalEntity(LegalEntity legalEntity)
        {
            throw new NotImplementedException();
        }
    }
}
