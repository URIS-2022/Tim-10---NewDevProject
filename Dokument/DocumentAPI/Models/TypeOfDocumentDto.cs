namespace DocumentAPI.Models
{
	/// <summary>
	/// Document type model
	/// </summary>
	public class TypeOfDocumentDto
	{
		/// <summary>
		/// ID of document type
		/// </summary>
		public Guid typeOfDocumentId { get; set; }

		/// <summary>
		/// Name of document type
		/// </summary>
		public string typeOfDocumentName { get; set; }

	}
}
