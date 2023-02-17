using AutoMapper;
using DocumentAPI.Models;
using DocumentAPI.Entities;

namespace DocumentAPI.Profiles
{
	public class DocumentProfile : Profile
	{
		public DocumentProfile()
		{
			CreateMap<DocumentEntity, DocumentDto>();
			CreateMap<DocumentCreationDto, DocumentEntity>();
			CreateMap<DocumentEntity, DocumentUpdateDto>();
			CreateMap<DocumentEntity, DocumentConfirmation>();
			CreateMap<DocumentEntity, DocumentEntity>();


		}

	}
}
