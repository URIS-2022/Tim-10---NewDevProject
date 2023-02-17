using AutoMapper;
using DocumentAPI.Models;
using DocumentAPI.Entities;

namespace DocumentAPI.Profiles
{
	public class TypeOfDocumentConfirmationProfile : Profile
	{
		public TypeOfDocumentConfirmationProfile()
		{
			CreateMap<TypeOfDocumentConfirmation, TypeOfDocumentConfirmationDto>();
			CreateMap<TypeOfDocumentConfirmation, TypeOfDocumentUpdateDto>();

		}

	}
}
