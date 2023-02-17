using AutoMapper;
using Personality.Models;

namespace Personality.Profiles
{
    public class PersonalityProfile : Profile
    {
        public PersonalityProfile()
        {
            CreateMap<Entities.Personality, PersonalityDto>()
                .ForMember(dest => dest.name,
                opt => opt.MapFrom(src => src.name + " " + src.surname));
            CreateMap<Entities.Personality, PersonalityCreateDto>();
            CreateMap<Entities.Personality, Entities.Personality>();
            CreateMap<PersonalityCreateDto, Entities.Personality>();
        }
    }
}
