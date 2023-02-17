using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class ClassProfile : Profile
    {
        public ClassProfile()
        {
            CreateMap<Class, ClassDto>();
            CreateMap<ClassDto, Class>();
            CreateMap<Class, Class>();
        }

    }
}
