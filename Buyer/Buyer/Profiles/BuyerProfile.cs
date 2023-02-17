using AutoMapper;
using Buyer.Entities;
using Buyer.Models;

namespace Buyer.Profiles
{
    public class BuyerProfile : Profile
    {
        public BuyerProfile()
        {
            CreateMap<BuyerModel, BuyerModelDto>();
            CreateMap<BuyerModelDto, BuyerModel>();
            CreateMap<BuyerConformation, BuyerConformationDto>();
            CreateMap<BuyerConformationDto, BuyerConformation>();
        }
    }
}
