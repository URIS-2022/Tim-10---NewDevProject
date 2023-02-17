namespace DocumentAPI.Models
{
	public class TypeOfDocumentUpdateDto
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
