namespace PublicBidding.Models
{
	/// <summary>
	/// DTO for public bidding status
	/// </summary>
	public class StatusOfPublicBiddingDto
	{
		/// <summary>
		/// Public bidding status id
		/// </summary>
		public Guid statusOfPublicBiddingId { get; set; }

		/// <summary>
		/// Public bidding status name
		/// </summary>
		public string statusOfPublicBiddingName { get; set; }
	}
}
