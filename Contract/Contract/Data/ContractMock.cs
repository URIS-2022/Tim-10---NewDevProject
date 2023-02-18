using Contract.Entities;

namespace Contract.Data
{
    public class ContractMock : IContractRepository
    {
        public static List<ContractEntity> Contract { get; set; } = new List<ContractEntity>();
        public ContractMock()
        {
            FillData();
        }

        private static void FillData()
        {
            Contract.AddRange(new List<ContractEntity>
            {
                new ContractEntity
                {
                    contractId = Guid.Parse("42889DFC-4E97-49B0-827E-80066DCF48A4"),
                    typeId = Guid.Parse("06CFBBBF-39E1-485C-BD54-3CB336F25242"),
                    documentId = Guid.Parse("E8522D7B-5261-4588-907F-4DBBA12D6AED"),
                    referenceNumber = "123/RS",
                    publicBiddingId = Guid.Parse("192CB74B-D8D9-4430-82A3-F585A7E89689"),
                    dateOfContract = DateTime.Parse("2022-12-12T10:00:00"),
                    buyerId = Guid.Parse("D14EE77F-B24C-4F06-9C6F-016552927E94"),
                    deadline = DateTime.Parse("2024-11-15T09:00:00"),
                    place = "Subotica",
                    dateOfSigning = DateTime.Parse("2022-12-12T10:00:00")

                },
                new ContractEntity
                {
                    contractId = Guid.Parse("EDF365DC-83F7-4402-B1C4-ECD794952FD4"),
                    typeId = Guid.Parse("5E6F0201-B31A-4767-8087-910E3C91DCC4"),
                    documentId = Guid.Parse("D450BE56-6CA0-4624-8673-21D9B57517AF"),
                    referenceNumber = "123/RS",
                    publicBiddingId = Guid.Parse("9128178C-B6BC-4C61-A58E-4D994EE9A4F5"),
                    dateOfContract = DateTime.Parse("2020-09-17T09:00:00"),
                    buyerId = Guid.Parse("BBDE3AF2-1804-43AE-9D83-AC631A72D6F5"),
                    deadline = DateTime.Parse("2023-12-27T09:00:00"),
                    place = "Novi Sad",
                    dateOfSigning = DateTime.Parse("2020-09-17T09:00:00")
                }
            });
        }




        public List<ContractEntity> GetContracts(string? referenceNumber = null)
        {
            return (from e in Contract
                    where string.IsNullOrEmpty(referenceNumber) || e.referenceNumber == referenceNumber
                    select e).ToList();
        }

        public ContractEntity GetContractById(Guid contractId)
        {
            return Contract.FirstOrDefault(e => e.contractId == contractId);
        }



        public ContractEntity CreateContract(ContractEntity contract)
        {
            contract.contractId = Guid.NewGuid();
            Contract.Add(contract);
            ContractEntity con = GetContractById(contract.contractId);

            return new ContractEntity
            {
                contractId = con.contractId,
                typeId = con.typeId,
                documentId = con.documentId,
                referenceNumber = con.referenceNumber,
                publicBiddingId = con.publicBiddingId,
                dateOfContract = con.dateOfContract,
                buyerId = con.buyerId,
                deadline = con.deadline,
                place = con.place,
                dateOfSigning = con.dateOfSigning
            };
        }


        public void UpdateContract(ContractEntity contract)
        {
            ContractEntity cont = GetContractById(contract.contractId);

            cont.contractId = contract.contractId;
            cont.typeId = contract.typeId;
            cont.documentId = contract.documentId;
            cont.referenceNumber = contract.referenceNumber;
            cont.publicBiddingId = contract.publicBiddingId;
            cont.dateOfContract = contract.dateOfContract;
            cont.buyerId = contract.buyerId;
            cont.deadline = contract.deadline;
            cont.place = contract.place;
            cont.dateOfSigning = contract.dateOfSigning;


        }

        public void DeleteContract(Guid contractId)
        {
            Contract.Remove(Contract.FirstOrDefault(e => e.contractId == contractId));
        }

        public bool SaveChanges()
        {
            return true;
        }
    }
}
