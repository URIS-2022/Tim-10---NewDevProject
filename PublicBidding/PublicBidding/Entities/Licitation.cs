using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBidding.Entities
{
	/// <summary>
	/// Licitation entity
	/// </summary>
	public class Licitation
	{
		/// <summary>
		/// Licitation id
		/// </summary>
		[Key]
		public Guid licitationId { get; set; }

		/// <summary>
		/// Number
		/// </summary>
		public int number { get; set; }

		/// <summary>
		/// Year
		/// </summary>
		public int year { get; set; }

		/// <summary>
		/// Date
		/// </summary>
		public DateTime date { get; set; }

		/// <summary>
		/// Restrictions
		/// </summary>
		public int restrictions { get; set; }

		/// <summary>
		/// Price difference
		/// </summary>
		public int priceDifference { get; set; }

		/// <summary>
		///Lista dokumentacije fizicka lica.
		/// </summary>
		[NotMapped]
		public List<string> listOfDocumentationOfIndividuals { get; set; } = default!;

		/// <summary>
		///Lista dokumentacije pravna lica.
		/// </summary>
		[NotMapped]
		public List<string> listOfDocumentationOfLegalEntities { get; set; } = default!;

		/// <summary>
		/// Public bidding id
		/// </summary>
		[ForeignKey("publicBidding")]
		public Guid publicBiddingId { get; set; }
		public PublicBidding publicBidding { get; set; }

		/// <summary>
		/// Deadline for submission of applications
		/// </summary>
		public DateTime deadlineForSubmissionOfApplications { get; set; }
	}
}
