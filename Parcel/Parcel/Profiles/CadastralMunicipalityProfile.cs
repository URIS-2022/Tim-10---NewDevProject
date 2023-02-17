using AutoMapper;
using Parcel.Entities;
using Parcel.Models;

namespace Parcel.Profiles
{
    public class CadastralMunicipalityProfile : Profile
    {
        public CadastralMunicipalityProfile()
        {
            CreateMap<CadastralMunicipality, CadastralMunicipalityDto>();
            CreateMap<CadastralMunicipalityDto, CadastralMunicipality>();
            CreateMap<CadastralMunicipality, CadastralMunicipality>();
        }

    }
}
