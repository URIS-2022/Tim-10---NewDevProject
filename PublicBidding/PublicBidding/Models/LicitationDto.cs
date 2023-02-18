namespace PublicBidding.Models
{
	/// <summary>
	/// DTO for updating licitation
	/// </summary>
	public class LicitationDto
	{
		/// <summary>
		/// Licitation id
		/// </summary>
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
		/// Public bidding id
		/// </summary>
		public Guid publicBiddingId { get; set; }

		/// <summary>
		/// Deadline for submission of applications
		/// </summary>
		public DateTime deadlineForSubmissionOfApplications { get; set; }
	}
}
