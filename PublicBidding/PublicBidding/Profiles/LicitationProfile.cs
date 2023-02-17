using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Profiles
{
	public class LicitationProfile : Profile

	{
		public LicitationProfile()
		{
			CreateMap<Licitation, LicitationDto>(); //prvo izvor pa destinacija
			CreateMap<LicitationCreationDto, Licitation>(); //koristi se u post metodi u kontroleru
			CreateMap<LicitationUpdateDto, Licitation>(); //koristi se u put metodi u kontroleru
			CreateMap<Licitation, Licitation>(); //koristi se u kontroleru
			CreateMap<Licitation, LicitationConfirmationDto>(); //koristi se u kontroleru
			CreateMap<LicitationConfirmationDto, LicitationConfirmationDto>(); //koristi se u kontroleru
		}

	}
}
