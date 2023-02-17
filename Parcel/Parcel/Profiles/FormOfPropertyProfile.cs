using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class FormOfPropertyProfile : Profile
    {
        public FormOfPropertyProfile()
        {
            CreateMap<FormOfProperty, FormOfPropertyDto>();
            CreateMap<FormOfPropertyDto, FormOfProperty>();
            CreateMap<FormOfProperty, FormOfProperty>();
        }

    }
}
