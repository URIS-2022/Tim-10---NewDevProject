using System.ComponentModel.DataAnnotations;

namespace PublicBidding.Entities
{
	/// <summary>
	/// Type of public bidding entity
	/// </summary>
	public class TypeOfPublicBidding
	{
		/// <summary>
		/// Public bidding type id
		/// </summary>
		[Key]
		public Guid typePublicBiddingId { get; set; }

		/// <summary>
		///Public bidding type name
		/// </summary>
		public string typePublicBiddingName { get; set; }
	}
}
