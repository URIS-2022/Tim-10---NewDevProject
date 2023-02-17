using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Profiles
{
	public class StatusOfPublicBiddingProfile : Profile

	{
		public StatusOfPublicBiddingProfile()
		{
			CreateMap<StatusOfPublicBidding, StatusOfPublicBiddingDto>(); //prvo izvor pa destinacija
			CreateMap<StatusOfPublicBiddingCreationDto, StatusOfPublicBidding>(); //koristi se u post metodi u kontroleru
			CreateMap<StatusOfPublicBiddingUpdateDto, StatusOfPublicBidding>(); //koristi se u put metodi u kontroleru
			CreateMap<StatusOfPublicBidding, StatusOfPublicBidding>(); //koristi se u kontroleru
			CreateMap<StatusOfPublicBidding, StatusOfPublicBiddingConfirmationDto>(); //koristi se u kontroleru
			CreateMap<StatusOfPublicBiddingConfirmationDto, StatusOfPublicBiddingConfirmationDto>(); //koristi se u kontroleru
		}

	}
}
