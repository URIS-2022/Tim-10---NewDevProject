using AutoMapper;
using Contract.Entities;
using Contract.Models;

namespace Contract.Profiles
{
    public class TypeOfGuaranteeProfile : Profile
    {
        public TypeOfGuaranteeProfile()
        {
            CreateMap<TypeOfGuaranteeEntity, TypeOfGuaranteeDto>();
            CreateMap<TypeOfGuaranteeDto, TypeOfGuaranteeEntity>();
            CreateMap<TypeOfGuaranteeEntity, TypeOfGuaranteeEntity>();
            CreateMap<TypeOfGuaranteeDto, TypeOfGuaranteeDto>();
        }
    }
}
