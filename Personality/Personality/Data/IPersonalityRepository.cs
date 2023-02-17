
namespace Personality.Data
{
    public interface IPersonalityRepository
    {
        List<Entities.Personality> GetPersonalityList();
        Entities.Personality GetPersonalityById(Guid personalityId); //vraca 1 prijavu po id-u
        Entities.Personality CreatePersonality(Entities.Personality personality); //kreiranje korisnika
        void UpdatePersonality(Entities.Personality personality); //update korisnika
        void DeletePersonality(Guid personalityId); //brisanje
        bool SaveChanges();
    }
}
