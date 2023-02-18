namespace PublicBidding.Models
{
	/// <summary>
	/// Model for creating licitation
	/// </summary>
	public class LicitationCreationDto
	{
		//izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

		/// <summary>
		/// Number
		/// </summary>
		/// [Required(ErrorMessage = "You must enter the licitation number")]
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
