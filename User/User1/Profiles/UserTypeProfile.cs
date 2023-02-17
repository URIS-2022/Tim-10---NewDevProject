using AutoMapper;
using User1.Entities;
using User1.Models;

namespace User1.Profiles
{
    public class UserTypeProfile : Profile
    {
        public UserTypeProfile()
        {
            CreateMap<UserTypeProfile, UserTypeDto>();

            CreateMap<UserTypeDto, UserType>();

            CreateMap<UserType, UserTypeDto>();
        }

    }
}
