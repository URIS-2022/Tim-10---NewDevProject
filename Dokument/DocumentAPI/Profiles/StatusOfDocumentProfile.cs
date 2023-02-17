using AutoMapper;
using DocumentAPI.Entities;
using DocumentAPI.Models;

namespace DocumentAPI.Profiles
{
	public class StatusOfDocumentProfile : Profile
	{
		public StatusOfDocumentProfile()
		{
			CreateMap<StatusOfDocumentEntity, StatusOfDocumentDto>();
			CreateMap<StatusOfDocumentCreationDto, StatusOfDocumentEntity>();
			CreateMap<StatusOfDocumentEntity, StatusOfDocumentUpdateDto>();
			CreateMap<StatusOfDocumentEntity, StatusOfDocumentConfirmation>();
			CreateMap<StatusOfDocumentEntity, StatusOfDocumentEntity>();
		}

	}
}
