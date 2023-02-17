using AutoMapper;
using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public class DocumentRepository : IDocumentRepository
	{

		private readonly DocumentContext context;
		private readonly IMapper mapper;

		public DocumentRepository(DocumentContext context, IMapper mapper)
		{
			this.context = context;
			this.mapper = mapper;
		}

		public bool SaveChanges()
		{
			return context.SaveChanges() > 0;
		}

		/// <summary>
		/// Get all documents
		/// </summary>
		/// <param name="referenceNumber">Filter field reference number zavodni</param>
		/// <returns></returns>
		public List<DocumentEntity> GetDocuments(string referenceNumber = null)
		{
			return context.DocumentEntity.Where(e => (referenceNumber == null)).ToList();
		}


		/// <summary>
		/// Get document by id
		/// </summary>
		/// <param name="documentId">Document id</param>
		/// <returns></returns>
		public DocumentEntity GetDocumentById(Guid documentId)
		{
			return context.DocumentEntity.FirstOrDefault(e => e.documentId == documentId);
		}

		/// <summary>
		/// Create document
		/// </summary>
		/// <param name="document">Document body</param>
		/// <returns></returns>
		public DocumentConfirmation CreateDocument(DocumentEntity document)
		{
			var createdEntity = context.Add(document);
			return mapper.Map<DocumentConfirmation>(createdEntity.Entity);
		}

		/// <summary>
		/// Update document
		/// </summary>
		/// <param name="document">Enter the document</param>

		public void UpdateDocument(DocumentEntity document)
		{
			//Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
			//i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
		}


		/// <summary>
		/// Delete document
		/// </summary>
		/// <param name="documentId">Document id</param>
		public void DeleteDocument(Guid documentId)
		{
			var document = GetDocumentById(documentId);
			context.Remove(document);
		}

	}
}
