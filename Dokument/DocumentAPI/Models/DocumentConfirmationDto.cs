namespace DocumentAPI.Models
{
	public class DocumentConfirmationDto
	{

		/// <summary>
		/// ID of document
		/// </summary>
		public Guid documentId { get; set; }

		/// <summary>
		/// ID of document status
		/// </summary>
		public Guid statusOfDocumentId { get; set; }

		/// <summary>
		/// ID of document type
		/// </summary>
		public Guid typeOfDocumentId { get; set; }

		/// <summary>
		/// Reference number
		/// </summary>
		public string referenceNumber { get; set; }

		/// <summary>
		/// Document creation date
		/// </summary>
		public DateTime date { get; set; }

		/// <summary>
		/// Document adoption date
		/// </summary>
		public DateTime dateAdoptionDocument { get; set; }
	}
}
