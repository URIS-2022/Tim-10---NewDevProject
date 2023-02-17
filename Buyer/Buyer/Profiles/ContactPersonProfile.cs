using AutoMapper;
using Buyer.Entities;
using Buyer.Models;

namespace Buyer.Profiles
{
    public class ContactPersonProfile : Profile
    {
        public ContactPersonProfile()
        {
            CreateMap<ContactPerson, ContactPersonDto>();
            CreateMap<ContactPersonDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPersonUpdateDto>();
            CreateMap<ContactPersonUpdateDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPersonConformationDto>();
            CreateMap<ContactPersonConformationDto, ContactPerson>();
            CreateMap<ContactPerson, ContactPerson>();
        }
    }
}
