using AutoMapper;
using PublicBidding.Models;

namespace PublicBidding.Profiles
{
	public class PublicBiddingProfile : Profile
	{
		public PublicBiddingProfile()
		{
			CreateMap<Entities.PublicBidding, PublicBiddingDto>(); //prvo izvor pa destinacija
			CreateMap<PublicBiddingCreationDto, Entities.PublicBidding>(); //koristi se u post metodi u kontroleru
			CreateMap<PublicBiddingUpdateDto, Entities.PublicBidding>(); //koristi se u put metodi u kontroleru
			CreateMap<Entities.PublicBidding, Entities.PublicBidding>(); //koristi se u kontroleru
			CreateMap<Entities.PublicBidding, PublicBiddingConfirmationDto>(); //koristi se u kontroleru
			CreateMap<PublicBiddingConfirmationDto, PublicBiddingConfirmationDto>(); //koristi se u kontroleru
		}

	}
}
