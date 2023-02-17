using AutoMapper;
using Buyer.Entities;

namespace Buyer.Data
{
    public class PriorityRepository : IPriorityRepository
    {
        private readonly BuyerContext context;
        private readonly IMapper mapper;

        public PriorityRepository(BuyerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public PriorityModel CreatePriority(PriorityModel priority)
        {
            priority.priorityId = Guid.NewGuid();
            var newEntity = context.priorities.Add(priority);
            return mapper.Map<PriorityModel>(newEntity.Entity);
        }

        public void DeletePriority(Guid pid)
        {
            PriorityModel priority = GetPriorityById(pid);
            context.priorities.Remove(priority);
        }

        public List<PriorityModel> GetPriority()
        {
            return context.priorities.ToList();
        }

        public PriorityModel GetPriorityById(Guid pid)
        {
            return context.priorities.FirstOrDefault(p => p.priorityId == pid);
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public PriorityModel UpdatePriority(PriorityModel priority)
        {
            throw new NotImplementedException();
        }
    }
}
