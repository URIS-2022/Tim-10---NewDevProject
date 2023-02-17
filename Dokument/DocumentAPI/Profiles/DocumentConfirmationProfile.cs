using DocumentAPI.Models;
using DocumentAPI.Entities;
using AutoMapper;

namespace DocumentAPI.Profiles
{
	public class DocumentConfirmationProfile : Profile
	{
		public DocumentConfirmationProfile() 
		{
			CreateMap<DocumentConfirmation, DocumentConfirmationDto>();
			CreateMap<DocumentConfirmation, DocumentUpdateDto>();
		}

	}
}
