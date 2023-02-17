using AuthorizedPerson.Entities;
using AuthorizedPerson.Models;
using AutoMapper;

namespace AuthorizedPerson.Profiles
{
    public class AuthorizedPersonProfile : Profile
    {
        public AuthorizedPersonProfile() {
            CreateMap<AuthorizedPersonModel, AuthorizedPersonDto>();
            CreateMap<AuthorizedPersonDto, AuthorizedPersonModel>();
            CreateMap<AuthorizedPersonConformation, AuthorizedPersonConformationDto>();
            CreateMap<AuthorizedPersonConformationDto, AuthorizedPersonConformation>();
            CreateMap<AuthorizedPersonModel, AuthorizedPersonModel>();
            CreateMap<AuthorizedPersonModel, AuthorizedPersonUpdateDto>();
            CreateMap<AuthorizedPersonUpdateDto, AuthorizedPersonModel>();
            CreateMap<AuthorizedPersonModel, AuthorizedPersonConformationDto>();
            CreateMap<AuthorizedPersonConformationDto, AuthorizedPersonModel>();
        }
    }
}
