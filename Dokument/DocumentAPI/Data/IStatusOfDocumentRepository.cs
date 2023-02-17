using DocumentAPI.Entities;

namespace DocumentAPI.Data
{
	public interface IStatusOfDocumentRepository
	{
		List<StatusOfDocumentEntity> GetStatusOfDocumentEntities(bool adopted = false);
		StatusOfDocumentEntity GetStatusOfDocumentById(Guid statusOfDocumentId);
		StatusOfDocumentConfirmation CreateStatusOfDocument(StatusOfDocumentEntity document);
		void UpdateStatusOfDocument(StatusOfDocumentEntity document);
		void DeleteStatusOfDocument(Guid statusOfDocumentId);
		bool SaveChanges();

	}
}
