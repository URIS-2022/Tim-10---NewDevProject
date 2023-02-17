using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public interface ITypeOfDocumentRepository
	{
		List<TypeOfDocumentEntity> GetTypeOfDocumentEntities(string typeOfDocumentName = "type");
		TypeOfDocumentEntity GetTypeOfDocumentById(Guid documentId);

		TypeOfDocumentConfirmation CreateTypeOfDocument(TypeOfDocumentEntity documentId);

		void UpdateTypeOfDocument(TypeOfDocumentEntity documentId);

		void DeleteTypeOfDocument(Guid documentId);

		bool SaveChanges();

	}
}
