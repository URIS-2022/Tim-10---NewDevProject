using DocumentAPI.Entities;
using System.Reflection.Metadata;

namespace DocumentAPI.Data
{
	public class DocumentMockRepository : IDocumentRepository
	{
		public static List<DocumentEntity> documentEntites { get; set; } = new List<DocumentEntity>();

		public DocumentMockRepository()
		{
			FillData();
		}

		private static void FillData()
		{
			documentEntites.AddRange(new List<DocumentEntity>
			{
				new DocumentEntity
				{
					documentId = Guid.Parse("1794d8c7-6c5c-4725-9d92-d819bdc07773"),
					statusOfDocumentId = Guid.Parse("f822c45b-2fd7-4fa6-98ec-64e31c0529e6"),
					typeOfDocumentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
					referenceNumber="15548/RS7",
					date=DateTime.Parse("2021-11-15T09:00:00"),
					dateAdoptionDocument=DateTime.Parse("2021-12-15T09:00:00")
				},
				new DocumentEntity
				{
					documentId= Guid.Parse("cfe84b37-bb6d-498d-a546-5dee8758ed1a"),
					statusOfDocumentId = Guid.Parse("044f3de0-a9dd-4c2e-b745-89976a1b2a36"),
					typeOfDocumentId = Guid.Parse("f822c45b-2fd7-4fa6-98ec-64e31c0529e6"),
					referenceNumber="17748/RS7",
					date=DateTime.Parse("2019-11-15T09:00:00"),
					dateAdoptionDocument=DateTime.Parse("2019-12-15T09:00:00")
				}
			});
		}

		public DocumentEntity GetDocumentById(Guid documentID)
		{
			return documentEntites.FirstOrDefault(D => D.documentId == documentID);
		}

		public List<DocumentEntity> GetDocuments(string referenceNumber = null)
		{
			return (from e in documentEntites
					where string.IsNullOrEmpty(referenceNumber) || e.referenceNumber == referenceNumber
					select e).ToList();
		}

		public DocumentConfirmation CreateDocument(DocumentEntity document)
		{
			document.documentId = Guid.NewGuid();
			document.statusOfDocumentId = Guid.NewGuid();
			document.typeOfDocumentId = Guid.NewGuid();
			documentEntites.Add(document);
			var d = GetDocumentById(document.documentId);

			return new DocumentConfirmation
			{
				documentId = d.documentId,
				statusOfDocumentId = (Guid)d.statusOfDocumentId,
				typeOfDocumentId = (Guid)d.typeOfDocumentId,
				referenceNumber = d.referenceNumber,
				date = d.date,
				dateAdoptionDocument = d.dateAdoptionDocument
			};
		}

		public void DeleteDocument(Guid documentId)
		{
			documentEntites.Remove(documentEntites.FirstOrDefault(e => e.documentId == documentId));
		}

		public void UpdateDocument(DocumentEntity document)
		{
			DocumentEntity doc = GetDocumentById(document.documentId);

			doc.documentId = document.documentId;
			doc.statusOfDocumentId = document.statusOfDocumentId;
			doc.typeOfDocumentId = document.typeOfDocumentId;
			doc.referenceNumber = document.referenceNumber;
			doc.date = document.date;
			doc.dateAdoptionDocument = document.dateAdoptionDocument;

		}
		public bool SaveChanges()
		{
			return true;
		}

	}
}
