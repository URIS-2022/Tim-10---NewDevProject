using AutoMapper;
using PublicBidding.Entities;
using PublicBidding.Models;

namespace PublicBidding.Profiles
{
	public class TypeOfPublicBiddingProfile : Profile

	{
		public TypeOfPublicBiddingProfile()
		{
			CreateMap<TypeOfPublicBidding, TypeOfPublicBiddingDto>(); //prvo izvor pa destinacija
			CreateMap<TypeOfPublicBiddingCreationDto, TypeOfPublicBidding>(); //koristi se u post metodi u kontroleru
			CreateMap<TypeOfPublicBiddingUpdateDto, TypeOfPublicBidding>(); //koristi se u put metodi u kontroleru
			CreateMap<TypeOfPublicBidding, TypeOfPublicBidding>(); //koristi se u put metodi u kontroleru
			CreateMap<TypeOfPublicBiddingConfirmationDto, TypeOfPublicBiddingConfirmationDto>(); //koristi se u put metodi u kontroleru
			CreateMap<TypeOfPublicBidding, TypeOfPublicBiddingConfirmationDto>(); //koristi se u put metodi u kontroleru
		}

	}
}
