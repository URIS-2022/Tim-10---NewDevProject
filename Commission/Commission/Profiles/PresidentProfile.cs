using AutoMapper;
using Commission.Entities;
using Commission.Models;

namespace Commission.Profiles
{
    public class PresidentProfile : Profile
    {
        public PresidentProfile() {
            CreateMap<PresidentEntity, PresidentDto>();
            CreateMap<PresidentDto, PresidentEntity>();
            CreateMap<PresidentEntity, PresidentEntity>();
            CreateMap<PresidentDto, PresidentDto>();
        }
    }
}
