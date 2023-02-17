using AutoMapper;
using DocumentAPI.Models;
using DocumentAPI.Entities;

namespace DocumentAPI.Profiles
{
	public class StatusOfDocumentConfirmationProfile : Profile
	{
		public StatusOfDocumentConfirmationProfile()
		{
			CreateMap<StatusOfDocumentConfirmation, StatusOfDocumentConfirmationDto>();
			CreateMap<StatusOfDocumentConfirmation, StatusOfDocumentUpdateDto>();
		}

	}
}
