using AutoMapper;
using Buyer.Entities;
using Buyer.Models;

namespace Buyer.Profiles
{
    public class LegalEntitiesProfile : Profile
    {
        public LegalEntitiesProfile()
        {
            CreateMap<LegalEntity, BuyerLegalEntitiesCreationDto>();
            CreateMap<BuyerLegalEntitiesCreationDto, LegalEntity>();
            CreateMap<LegalEntity, LegalEntityUpdateDto>();
            CreateMap<LegalEntityUpdateDto, LegalEntity>();
            CreateMap<LegalEntity, LegalEntity>();
            CreateMap<LegalEntity, BuyerConformationDto>();
            CreateMap<BuyerConformationDto, LegalEntity>();
        }
    }
}
