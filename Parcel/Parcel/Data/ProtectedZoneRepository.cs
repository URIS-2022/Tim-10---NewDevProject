using AutoMapper;
using Parcel.Entities;

namespace Parcel.Data
{
    public class ProtectedZoneRepository: IProtectedZoneRepository
    {
        private readonly ParcelContext ?context;
        private readonly IMapper mapper;
        public ProtectedZoneRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public ProtectedZone CreateProtectedZone(ProtectedZone protectedZone)
        {
            var createdEntity = context.Add(protectedZone);
            context.SaveChanges();
            return mapper.Map<ProtectedZone>(createdEntity.Entity);
        }

        public void DeleteProtectedZone(Guid protectedZoneId)
        {
            var protectedZone = GetProtectedZoneById(protectedZoneId);
            context.Remove(protectedZone);
            context.SaveChanges();
        }
        public ProtectedZone GetProtectedZoneById(Guid protectedZoneId)
        {
            return context.ProtectedZone.FirstOrDefault(z => z.protectedZoneId == protectedZoneId);
        }

        public List<ProtectedZone> GetProtectedZoneList()
        {
            return context.ProtectedZone.ToList();
        }
        public void UpdateProtectedZone(ProtectedZone protectedZone)
        {
            //nije potrebno posebno implementirati update
        }
    }
}
