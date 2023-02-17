using AutoMapper;
using Buyer.Entities;

namespace Buyer.Data
{
    public class IndividualRepository : IIndividialRepository
    {
        private readonly BuyerContext context;
        private readonly IMapper mapper;

        public IndividualRepository(BuyerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Individual CreateIndividual(Individual individual)
        {
            individual.buyerId = Guid.NewGuid();
            var newEntity = context.individuals.Add(individual);
            return mapper.Map<Individual>(newEntity.Entity);
        }

        public void DeleteIndividual(Guid iid)
        {
            Individual individual = GetIndividualById(iid);
            context.individuals.Remove(individual);
        }

        public List<Individual> GetIndividual()
        {
            return context.individuals.ToList();
        }

        public Individual GetIndividualById(Guid iid)
        {
            return context.individuals.FirstOrDefault(i => i.buyerId == iid);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public Individual UpdateIndividual(Individual individual)
        {
            throw new NotImplementedException();
        }
    }
}
