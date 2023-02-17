using AutoMapper;
using Country.Entities;
using Country.Models;

namespace Country.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
            CreateMap<Address, Address>();


        }

    }
}
