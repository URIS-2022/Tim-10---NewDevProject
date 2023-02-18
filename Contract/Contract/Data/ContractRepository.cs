using AutoMapper;
using Contract.Entities;

namespace Contract.Data
{
    public class ContractRepository : IContractRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public ContractRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ContractEntity CreateContract(ContractEntity contract)
        {
            contract.contractId = Guid.NewGuid();
            var createdEntity = context.Add(contract);
            return mapper.Map<ContractEntity>(createdEntity.Entity);
        }

        public void DeleteContract(Guid contractId)
        {
            var document = GetContractById(contractId);
            context.Remove(document);
        }

        public List<ContractEntity> GetContracts(string? referenceNumber = null)
        {
            return context.ContractEntity.Where(e => (referenceNumber == null)).ToList();
        }

        public ContractEntity GetContractById(Guid contractId)
        {
            return context.ContractEntity.FirstOrDefault(e => e.contractId == contractId);
        }



        public void UpdateContract(ContractEntity contract)
        {
            //does not need to be implemented
            
        }

       
    }
}
