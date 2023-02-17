using AutoMapper;
using DocumentAPI.Models;
using DocumentAPI.Entities;

namespace DocumentAPI.Profiles
{
	public class TypeOfDocumentProfile : Profile
	{
		public TypeOfDocumentProfile()
		{
			CreateMap<TypeOfDocumentEntity, TypeOfDocumentDto>();
			CreateMap<TypeOfDocumentCreationDto, TypeOfDocumentEntity>();
			CreateMap<TypeOfDocumentUpdateDto, TypeOfDocumentEntity>();
			CreateMap<TypeOfDocumentEntity, TypeOfDocumentConfirmation>();
			CreateMap<TypeOfDocumentEntity, TypeOfDocumentEntity>();

		}

	}
}
