using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class WorkabilityProfile : Profile
    {
        public WorkabilityProfile()
        {
            CreateMap<Workability, WorkabilityDto>();
            CreateMap<WorkabilityDto, Workability>();
            CreateMap<Workability, Workability>();
        }
    }
}
