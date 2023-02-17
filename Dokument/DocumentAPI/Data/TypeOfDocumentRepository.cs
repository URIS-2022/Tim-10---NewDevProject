using AutoMapper;
using DocumentAPI.Entities;
using System.Reflection.Metadata;

namespace DocumentAPI.Data
{
	public class TypeOfDocumentRepository : ITypeOfDocumentRepository
	{
		private readonly DocumentContext context;
		private readonly IMapper mapper;

		public TypeOfDocumentRepository(DocumentContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		/// <summary>
		/// Get all type of documents
		/// </summary>
		/// <param name="typeOfDocumentName">Filter parametar name</param>
		/// <returns></returns>
		public List<TypeOfDocumentEntity> GetTypeOfDocumentEntities(string typeOfDocumentName = "type")
		{
			return context.TypeOfDocumentEntity.Where(e => (typeOfDocumentName == "type")).ToList();
		}

		/// <summary>
		/// Get type of document by id
		/// </summary>
		/// <param name="typeOfDocumentId">Body of document id type</param>
		/// <returns></returns>

		public TypeOfDocumentEntity GetTypeOfDocumentById(Guid typeOfDocumentId)
		{
			return context.TypeOfDocumentEntity.FirstOrDefault(e => e.typeOfDocumentId == typeOfDocumentId);
		}


		/// <summary>
		/// Create type of document
		/// </summary>
		/// <param name="typeOfDocument">Body of document</param>
		/// <returns></returns>
		public TypeOfDocumentConfirmation CreateTypeOfDocument(TypeOfDocumentEntity typeOfDocument)
		{
			var createdEntity = context.Add(typeOfDocument);
			return mapper.Map<TypeOfDocumentConfirmation>(createdEntity.Entity);
		}

		/// <summary>
		/// Update type of document
		/// </summary>
		/// <param name="typeOfDocumentId">Enter document id</param>
		public void UpdateTypeOfDocument(TypeOfDocumentEntity typeOfDocumentId)
		{
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
		}


		/// <summary>
		/// Delete type of document
		/// </summary>
		/// <param name="documentId">Enter document id</param>
		public void DeleteTypeOfDocument(Guid typeOfDocumentId)
		{
			var documentType = GetTypeOfDocumentById(typeOfDocumentId);
			context.Remove(documentType);
		}

	}
}
