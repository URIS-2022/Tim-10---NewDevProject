using System.ComponentModel.DataAnnotations;

namespace PublicBidding.Entities
{
	/// <summary>
	/// Status of public bidding entity
	/// </summary>
	public class StatusOfPublicBidding
	{
		/// <summary>
		/// Public bidding status id
		/// </summary>
		[Key]
		public Guid statusOfPublicBiddingId { get; set; }

		/// <summary>
		/// Public bidding status name
		/// </summary>
		public string statusOfPublicBiddingName { get; set; }

	}
}
