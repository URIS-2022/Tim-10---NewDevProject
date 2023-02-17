using AutoMapper;
using Commission.Entities;
using Commission.Models;

namespace Commission.Profiles
{
    public class CommissionProfile : Profile
    {
        public CommissionProfile() {
            CreateMap<CommissionEntity, CommissionDto>();
            CreateMap<CommissionDto, CommissionEntity>();
            CreateMap<CommissionEntity, CommissionEntity>();
        }
    }
}
