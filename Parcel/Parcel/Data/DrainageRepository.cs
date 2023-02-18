using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class DrainageRepository: IDrainageRepository
    {

        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public DrainageRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Drainage CreateDrainage(Drainage drainage)
        {
            var createdEntity = context.Add(drainage);
            context.SaveChanges();
            return mapper.Map<Drainage>(createdEntity.Entity);
        }

        public void DeleteDrainage(Guid drainageId)
        {
            var drainage = GetDrainageById(drainageId);
            context.Remove(drainage);
            context.SaveChanges();
        }

        public Drainage GetDrainageById(Guid drainageId)
        {
            return context.Drainage.FirstOrDefault(d => d.drainageId == drainageId);
        }

        public List<Drainage> GetDrainageList()
        {
            return context.Drainage.ToList();
        }
        public void UpdateDrainage(Drainage drainage)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
