namespace DocumentAPI.Entities
{
	public class DocumentConfirmation
	{
		public Guid documentId { get; set; }
		public Guid statusOfDocumentId { get; set; }
		public Guid typeOfDocumentId { get; set; }
		public string referenceNumber { get; set; }
		public DateTime date { get; set; }
		public DateTime dateAdoptionDocument { get; set; }

	}
}
