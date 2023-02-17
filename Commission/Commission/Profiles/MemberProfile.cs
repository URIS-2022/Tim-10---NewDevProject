using Commission.Entities;
using Commission.Models;
using AutoMapper;

namespace Commission.Profiles
{
    public class MemberProfile : Profile
    {
        public MemberProfile() {
            CreateMap<MemberEntity, MemberDto>();
            CreateMap<MemberDto, MemberEntity> ();
            CreateMap<MemberEntity, MemberEntity>();
        }
        
    }
}
