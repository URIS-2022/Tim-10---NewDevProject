using AutoMapper;
using complaint.Entities;



namespace complaint.Data
{
    public class ActionRepository : IActionRepository
    {
        private readonly ComplaintContext context;
        private readonly IMapper mapper;
        public ActionRepository(ComplaintContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Entities.Action CreateAction(Entities.Action action)
        {
            var createdEntity = context.Add(action);
            context.SaveChanges();
            return mapper.Map<Entities.Action>(createdEntity.Entity);
        }

        public void DeleteAction(Guid actionId)
        {
            var action = GetActionById(actionId);
            context.Remove(action);
            context.SaveChanges();
        }

        public Entities.Action GetActionById(Guid actionId)
        {
            return context.Action.FirstOrDefault(d => d.actionId == actionId);
        }

        public List<Entities.Action> GetActionList()
        {
            return context.Action.ToList();
        }

        public void UpdateAction(Entities.Action action)
        {
            //nije potrebno posebno implementirati update
        }


    }
}
