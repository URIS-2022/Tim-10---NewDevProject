using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class CultureProfile : Profile
    {
        public CultureProfile()
        {
            CreateMap<Culture, CultureDto>();
            CreateMap<CultureDto, Culture>();
            CreateMap<Culture, Culture>();
        }

    }
}
