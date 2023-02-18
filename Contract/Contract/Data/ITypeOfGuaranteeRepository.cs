using Contract.Entities;

namespace Contract.Data
{
    public interface ITypeOfGuaranteeRepository
    {
        List<TypeOfGuaranteeEntity> GetGuarantees(string? type = null);

        TypeOfGuaranteeEntity GetGuaranteeById(Guid typeId);

        TypeOfGuaranteeEntity CreateGuarantee(TypeOfGuaranteeEntity guarantee);

        void UpdateGuarantee(TypeOfGuaranteeEntity guarantee);

        void DeleteGuarantee(Guid typeId);

        bool SaveChanges();
    }
}
