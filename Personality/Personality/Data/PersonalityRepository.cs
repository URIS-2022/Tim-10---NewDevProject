using AutoMapper;
using Personality.Entities;

namespace Personality.Data
{
    public class PersonalityRepository : IPersonalityRepository
    {
        private readonly PersonalityContext context;
        private readonly IMapper mapper;

        public PersonalityRepository(PersonalityContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public List<Entities.Personality> GetPersonalityList()
        {
           return context.Personalities.ToList();
        }
        public Entities.Personality GetPersonalityById(Guid personalityId)
        {
            return context.Personalities.FirstOrDefault(e => e.personalityId == personalityId);
        }

        public Entities.Personality CreatePersonality(Entities.Personality personality)
        {
            var createdEntity = context.Add(personality);
            return mapper.Map<Entities.Personality>(createdEntity.Entity);
        }

        public void UpdatePersonality(Entities.Personality personality)
        {
            throw new NotImplementedException();
        }

        public void DeletePersonality(Guid personalityId)
        {
            throw new NotImplementedException();
        }
    }
}
