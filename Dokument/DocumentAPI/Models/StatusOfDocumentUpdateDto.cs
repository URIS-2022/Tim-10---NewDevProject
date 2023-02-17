namespace DocumentAPI.Models
{
	public class StatusOfDocumentUpdateDto
	{
		public Guid statusOfDocumentId { get; set; }
		public bool adopted { get; set; }
		public bool rejected { get; set; }
		public bool opened { get; set; }	
		public bool modified { get; set; }

	}
}
