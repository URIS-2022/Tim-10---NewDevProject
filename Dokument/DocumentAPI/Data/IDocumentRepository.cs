using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public interface IDocumentRepository
	{

		List<DocumentEntity> GetDocuments(string referenceNumber = null);

		DocumentEntity GetDocumentById(Guid documentId);

		DocumentConfirmation CreateDocument(DocumentEntity document);

		void UpdateDocument(DocumentEntity document);

		void DeleteDocument(Guid documentId);

		bool SaveChanges();

	}
}
