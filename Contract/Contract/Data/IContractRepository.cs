using Contract.Entities;

namespace Contract.Data
{
    public interface IContractRepository
    {
        List<ContractEntity> GetContracts(string? referenceNumber = null);

        ContractEntity GetContractById(Guid contractId);

        ContractEntity CreateContract(ContractEntity contract);

        void UpdateContract(ContractEntity contract);

        void DeleteContract(Guid contractId);

        bool SaveChanges();
    }
}
