using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class WorkabilityRepository: IWorkabilityRepository
    {

        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public WorkabilityRepository(ParcelContext context,IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Workability CreateWorkability(Workability workability)
        {
            var createdEntity = context.Add(workability);
            context.SaveChanges();
            return mapper.Map<Workability>(createdEntity.Entity);
        }

        public void DeleteWorkability(Guid workabilityId)
        {
            var workability = GetWorkabilityById(workabilityId);
            context.Remove(workability);
            context.SaveChanges();
        }
        public Workability GetWorkabilityById(Guid workabilityId)
        {
            return context.Workability.FirstOrDefault(o => o.workabilityId == workabilityId);
        }

        public List<Workability> GetWorkabilityList()
        {
            return context.Workability.ToList();
        }
        public void UpdateWorkability(Workability workability)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
