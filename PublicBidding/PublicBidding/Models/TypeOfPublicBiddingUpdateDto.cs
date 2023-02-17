namespace PublicBidding.Models
{
	/// <summary>
	/// Model for update public bidding type
	/// </summary>
	public class TypeOfPublicBiddingUpdateDto
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
