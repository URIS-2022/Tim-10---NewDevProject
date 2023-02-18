using Buyer.Entities;

namespace Buyer.Data
{
    public class LegalEntityMockRepository : ILegalEntityRepository
    {
        public static List<LegalEntity> legalEntities { get; set; } = new List<LegalEntity>();
        public LegalEntityMockRepository() {
            FillData();
        } 
        private static void FillData()
        {
            legalEntities.AddRange(new List<LegalEntity>
            {
                new LegalEntity
                {
                buyerId = Guid.Parse("2F108769-AAF3-4829-9263-72523BBB223E"),
                buyerType = false,
                area = "155000",
                ban = true,
                banStartingDate = DateTime.Parse("2022-01-01T09:00:00"),
                banLasting = "355",
                banEndingDate = DateTime.Parse("2023-01-01T09:00:00"),
                authorizedPersonId = Guid.Parse("4F22E39E-3E7D-4063-AECE-CB9BF65B37CE"),
                priorityId = Guid.Parse("1BB9CB0A-A2AD-4FF3-BBAA-BA312E968A9B"),
                phoneNumber1 = "2345435675",
                phoneNumber2 = "8974839473",
                emailAddress = "34455@gmail.com",
                addressId = "addresstestno2",
                paymentId = "vvvvvvvvvvvvvvv",
                publicBiddingId = "bidding2",
                accountNumber = "23534234563",
                legalEntityName = "name",
                legalEntityId = "12432434",
                legalEntityFax = "fax",
                contactPerson = Guid.Parse("E1ED563F-E902-4D84-92C9-AE1E066952A2")
                }
            });
        }

        public LegalEntity CreateLegalEntity(LegalEntity legalEntity)
        {
            legalEntity.buyerId = Guid.NewGuid();
            legalEntities.Add(legalEntity);
            LegalEntity le = GetLegalEntityById(legalEntity.buyerId);

            return new LegalEntity
            {
                buyerId = le.buyerId
            };
        }

        public void DeleteLegalEntity(Guid leid)
        {
            legalEntities.Remove(legalEntities.FirstOrDefault(le => le.buyerId == leid));
        }

        public List<LegalEntity> GetLegalEntity()
        {
            return (from le in legalEntities select le).ToList();
        }

        public LegalEntity GetLegalEntityById(Guid leid)
        {
            return legalEntities.FirstOrDefault(le => le.buyerId == leid);
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public LegalEntity UpdateLegalEntity(LegalEntity legalEntity)
        {
            LegalEntity le = GetLegalEntityById(legalEntity.buyerId);

            le.buyerId = legalEntity.buyerId;
            le.buyerType = legalEntity.buyerType;
            le.area = legalEntity.area;
            le.ban = legalEntity.ban;
            le.banStartingDate = legalEntity.banStartingDate;
            le.banLasting = legalEntity.banLasting;
            le.banEndingDate = legalEntity.banEndingDate;
            le.authorizedPersonId = legalEntity.authorizedPersonId;
            le.priorityId = legalEntity.priorityId;
            le.phoneNumber1 = legalEntity.phoneNumber1;
            le.phoneNumber2 = legalEntity.phoneNumber2;
            le.accountNumber = legalEntity.accountNumber;
            le.addressId = legalEntity.addressId;
            le.paymentId = legalEntity.paymentId;
            le.publicBiddingId = legalEntity.publicBiddingId;
            le.emailAddress = legalEntity.emailAddress;
            le.legalEntityFax= legalEntity.legalEntityFax;
            le.legalEntityId= legalEntity.legalEntityId;
            le.legalEntityName= legalEntity.legalEntityName;

            return new LegalEntity
            {
                buyerId = le.buyerId
            };
           
        }
    }
}
