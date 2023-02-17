namespace PublicBidding.Models
{
	/// <summary>
	/// Model for updating public bidding status
	/// </summary>
	public class StatusOfPublicBiddingUpdateDto
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
