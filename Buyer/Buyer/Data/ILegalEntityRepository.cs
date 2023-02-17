using Buyer.Entities;

namespace Buyer.Data
{
    public interface ILegalEntityRepository
    {
        List<LegalEntity> GetLegalEntity();
        LegalEntity GetLegalEntityById(Guid leid);
        LegalEntity CreateLegalEntity(LegalEntity legalEntity);
        LegalEntity UpdateLegalEntity(LegalEntity legalEntity);
        void DeleteLegalEntity(Guid leid);
        bool SaveChanges();
    }
}
