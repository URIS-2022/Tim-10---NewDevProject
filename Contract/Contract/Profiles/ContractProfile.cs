using AutoMapper;
using Contract.Entities;
using Contract.Models;

namespace Contract.Profiles
{
    public class ContractProfile : Profile
    {
        public ContractProfile() {
            CreateMap<ContractEntity, ContractDto>();
            CreateMap<ContractDto, ContractEntity>();
            CreateMap<ContractEntity, ContractEntity>();
            CreateMap<ContractDto, ContractDto>();  
        }
    }
}
