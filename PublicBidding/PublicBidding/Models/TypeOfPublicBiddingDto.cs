namespace PublicBidding.Models
{
	/// <summary>
	/// DTO for public bidding type
	/// </summary>

	public class TypeOfPublicBiddingDto
	{
		/// <summary>
		/// Public bidding type id
		/// </summary>
		public Guid typePublicBiddingId { get; set; }

		/// <summary>
		///Public bidding type name
		/// </summary>
		public string typePublicBiddingName { get; set; }
	}
}
