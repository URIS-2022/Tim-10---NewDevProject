using Buyer.Entities;

namespace Buyer.Data
{
    public class IndividualMockRepository : IIndividialRepository
    {
        public static List<Individual> individuals { get; set; } = new List<Individual>();

        public IndividualMockRepository()
        {
            FillData();
        }
        private static void FillData()
        {
            individuals.AddRange(new List<Individual>
            {
                new Individual
                {
                buyerId = Guid.Parse("F5A13586-1A5C-4FEA-ADBE-0F352CA13371"),
                buyerType = true,
                area = "15000",
                ban = false,
                banStartingDate = DateTime.Parse("1900-01-01T09:00:00"),
                banLasting = "0",
                banEndingDate = DateTime.Parse("1900-01-01T09:00:00"),
                authorizedPersonId = Guid.Parse("4F22E39E-3E7D-4063-AECE-CB9BF65B37CE"),
                priorityId = Guid.Parse("12C7B642-416E-4358-90CA-9DDB67336F63"),
                phoneNumber1 = "2131231412",
                phoneNumber2 = "8974839473",
                emailAddress = "123@gmail.com",
                addressId = "addresstesttt",
                paymentId = "111111111111",
                publicBiddingId = "bidding1",
                individualName = "Amila",
                individualSurname = "Salihbegovic",
                individualId = "280100798916",
                accountNumber = "2489de9e32"
                }
            });
        }
        public Individual CreateIndividual(Individual individual)
        {
            individual.buyerId = Guid.NewGuid();
            individuals.Add(individual);
            Individual i = GetIndividualById(individual.buyerId);

            return new Individual
            {
                buyerId = i.buyerId
            };
        }

        public void DeleteIndividual(Guid iid)
        {
            individuals.Remove(individuals.FirstOrDefault(i => i.buyerId == iid));
        }

        public List<Individual> GetIndividual()
        {
            return (from i in individuals select i).ToList();
        }

        public Individual GetIndividualById(Guid iid)
        {
            return individuals.FirstOrDefault(i => i.buyerId == iid);
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Individual UpdateIndividual(Individual individual)
        {
            Individual i = GetIndividualById(individual.buyerId);

            i.buyerId = individual.buyerId;
            i.buyerType = individual.buyerType;
            i.area = individual.area;
            i.ban = individual.ban;
            i.banStartingDate = individual.banStartingDate;
            i.banLasting = individual.banLasting;
            i.banEndingDate = individual.banEndingDate;
            i.authorizedPersonId = individual.authorizedPersonId;
            i.priorityId = individual.priorityId;
            i.phoneNumber1 = individual.phoneNumber1;
            i.phoneNumber2 = individual.phoneNumber2;
            i.accountNumber = individual.accountNumber;
            i.addressId = individual.addressId;
            i.paymentId = individual.paymentId;
            i.publicBiddingId = individual.publicBiddingId;
            i.emailAddress = individual.emailAddress;
            i.individualName = individual.individualName;
            i.individualId = individual.individualId;
            i.individualSurname = individual.individualSurname;

            return new Individual
            {
                buyerId = i.buyerId
            };
        }
    }
}
