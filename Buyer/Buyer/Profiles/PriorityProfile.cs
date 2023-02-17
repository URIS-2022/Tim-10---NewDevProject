using AutoMapper;
using Buyer.Entities;
using Buyer.Models;

namespace Buyer.Profiles
{
    public class PriorityProfile : Profile
    {
        public PriorityProfile()
        {
            CreateMap<PriorityModel, BuyerPriorityDto>();
            CreateMap<BuyerPriorityDto, PriorityModel>();
            CreateMap<PriorityModel, PriorityUpdateDto>();
            CreateMap<PriorityUpdateDto, PriorityModel>();
            CreateMap<PriorityModel, PriorityConformationDto>();
            CreateMap<PriorityConformationDto, PriorityModel>();
            CreateMap<PriorityModel, PriorityModel>();
        
        }
    }
}
