using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class ProtectedZoneProfile : Profile
    {
        public ProtectedZoneProfile()
        {
            CreateMap<ProtectedZone, ProtectedZoneDto>();
            CreateMap<ProtectedZoneDto, ProtectedZone>();
            CreateMap<ProtectedZone, ProtectedZone>();
        }
    }
}
