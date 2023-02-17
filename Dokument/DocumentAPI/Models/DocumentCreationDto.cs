using System.ComponentModel.DataAnnotations;

namespace DocumentAPI.Models
{
	public class DocumentCreationDto
	{

		/// <summary>
		/// ID of document
		/// </summary>
		public Guid documentID { get; set; }

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
		[Required(ErrorMessage = "You must enter the reference number!")]
		public string referenceNumber { get; set; }

		/// <summary>
		/// Document creation date
		/// </summary>
		public DateTime date { get; set; }

		/// <summary>
		/// Document adoption date
		/// </summary>
		public DateTime dateAdoptionDocument { get; set; }

		/// <summary>
		/// Document date validation
		/// </summary>
		/// <param name="validationContext"></param>
		/// <returns></returns>
		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if (dateAdoptionDocument < date)
			{
				yield return new ValidationResult("The date of adoption of the document must be after the date of issue",
					new[] { "DocumentCreationDto" });
			}
		}

	}
}
