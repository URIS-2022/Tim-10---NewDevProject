using AutoMapper;
using Parcel.Entities;
using System.Net;

namespace Parcel.Data
{
    public class ParcelRepository: IParcelRepository
    {

        private readonly ParcelContext ?context;
        private readonly IMapper mapper;

        public ParcelRepository(ParcelContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Entities.Parcel CreateParcel(Entities.Parcel parcel)
        {
            var createdEntity = context.Add(parcel);
            context.SaveChanges();
            return mapper.Map<Entities.Parcel>(createdEntity.Entity);
        }

        public void DeleteParcel(Guid parcelId)
        {
            var parcel = GetParcelById(parcelId);
            context.Remove(parcel);
            context.SaveChanges();
        }

        public Entities.Parcel GetParcelById(Guid parcelId)
        {
            return context.Parcel.FirstOrDefault(p => p.parcelId == parcelId);
        }

        public List<Entities.Parcel> GetParcelList()
        {
            return context.Parcel.ToList();
        }

        public void UpdateParcel(Entities.Parcel parcel)
        {
            //nije potrebno implementirati posebno update
        }
    }
}
