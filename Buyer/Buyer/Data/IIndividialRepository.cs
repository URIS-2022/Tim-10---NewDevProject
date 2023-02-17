using Buyer.Entities;

namespace Buyer.Data
{
    public interface IIndividialRepository
    {
        List<Individual> GetIndividual();
        Individual GetIndividualById(Guid iid);
        Individual CreateIndividual(Individual individual);
        Individual UpdateIndividual(Individual individual);
        void DeleteIndividual(Guid iid);
        bool SaveChanges();

    }
}
