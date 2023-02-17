using AutoMapper;
using Commission.Entities;
using Commission.Models;

namespace Commission.Data
{
    public class PresidentRepository : IPresidentRepository
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public PresidentRepository(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public PresidentDto CreatePresident(PresidentEntity president)
        {
            var createdEntity = context.Add(president);
            return mapper.Map<PresidentDto>(createdEntity.Entity);
        }


        public void DeletePresident(Guid presidentId)
        {
            var president = GetPresidentById(presidentId);
            context.Remove(president);
        }

        public List<PresidentEntity> GetAllPresidents()
        {
            return context.President.ToList();
        }

        public PresidentEntity GetPresidentById(Guid presidentId)
        {
            return context.President.FirstOrDefault(r => r.presidentId == presidentId);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    }
}
