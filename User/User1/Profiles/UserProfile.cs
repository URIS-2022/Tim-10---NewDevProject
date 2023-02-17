using AutoMapper;
using User1.Entities;
using User1.Models;

namespace User1.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>();

            CreateMap<UserCreateDto, User>();
        }

    }
}
