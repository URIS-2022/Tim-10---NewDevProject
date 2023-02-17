using AutoMapper;
using Country.Entities;
using Country.Models;

namespace Country.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<Country1, CountryDto>();
            CreateMap<CountryDto, Country1>();
            CreateMap<Country1, Country1>();
        }

    }
}
