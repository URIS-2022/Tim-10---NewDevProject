using AutoMapper;
using Buyer.Entities;
using Buyer.Models;

namespace Buyer.Profiles
{
    public class IndividualProfile : Profile
    {
        public IndividualProfile() {
            CreateMap<Individual, BuyerIndividualCreationDto>();
            CreateMap<BuyerIndividualCreationDto, Individual>();
            CreateMap<Individual, IndividualUpdateDto>();
            CreateMap<IndividualUpdateDto, Individual>();
            CreateMap<Individual, Individual>();
            CreateMap<Individual, BuyerConformationDto>();
            CreateMap<BuyerConformationDto, Individual>();
        }
    }
}
