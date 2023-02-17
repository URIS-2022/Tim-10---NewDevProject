using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class DrainageProfile : Profile
    {
        public DrainageProfile()
        {
            CreateMap<Drainage, DrainageDto>();
            CreateMap<DrainageDto, Drainage>();
            CreateMap<Drainage, Drainage>();
        }
    }
}
