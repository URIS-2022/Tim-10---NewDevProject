using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DocumentAPI.Entities
{
	public class DocumentEntity
	{

		[Key]
		public Guid documentId { get; set; }
		[Required]
		[ForeignKey("StatusOfDocumentEntity")]
		public Guid? statusOfDocumentId { get; set; }
		public StatusOfDocumentEntity StatusOfDocumentEntity { get; set; }
		[Required]
		[ForeignKey("TypeOfDocumentEntity")]
		public Guid? typeOfDocumentId { get; set; }
		public TypeOfDocumentEntity TypeOfDocumentEntity { get; set; }
		public string referenceNumber { get; set; }
		public Guid userId { get; set; }
		public DateTime date { get; set; }
		public DateTime dateAdoptionDocument { get; set; }


		override
	   public string ToString()
		{
			return "Document: { documentId: " + this.documentId + ", statusOfDocumentId: " + this.statusOfDocumentId + ", typeOfDocumentId: " + this.typeOfDocumentId + ", referenceNumber: " + this.referenceNumber + ", date: " + this.date + ", dateAdoptionDocument: " + this.dateAdoptionDocument + " }";
		}

	}
}
