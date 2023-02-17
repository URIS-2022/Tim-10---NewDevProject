using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class ParcelProfile : Profile
    {
        public ParcelProfile()
        {
            CreateMap<Entities.Parcel, ParcelDto>();
            CreateMap<ParcelDto, Entities.Parcel>();
            CreateMap<Entities.Parcel, Entities.Parcel>();
        }
    }
}
