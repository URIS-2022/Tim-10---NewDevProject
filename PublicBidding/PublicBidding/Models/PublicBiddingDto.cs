namespace PublicBidding.Models
{
	/// <summary>
	/// DTO for public bidding
	/// </summary>
	public class PublicBiddingDto
	{
		/// <summary>
		/// Public bidding id
		/// </summary>
		public Guid publicBiddingId { get; set; }

		/// <summary>
		/// The date of holding the public bidding
		/// </summary>
		public DateTime date { get; set; }

		/// <summary>
		/// The time of the beginning of the public bidding
		/// </summary>
		public DateTime timeOfBeginning { get; set; }

		/// <summary>
		/// The time of the end of the public bidding
		/// </summary>
		public DateTime timeOfEnd { get; set; }

		/// <summary>
		/// Initial price per hectare
		/// </summary>
		public int initialPricePerHectare { get; set; }

		/// <summary>
		/// Exceptions
		/// </summary>
		public bool excepted { get; set; }

		/// <summary>
		/// Type of public bidding id
		/// </summary>
		public Guid typePublicBiddingId { get; set; }

		/// <summary>
		/// Auctioned price
		/// </summary>
		public int auctionedPrice { get; set; }

		/// <summary>
		/// Lease period
		/// </summary>
		public int leasePeriod { get; set; }

		/// <summary>
		/// Number of participants in the public bidding
		/// </summary>
		public int numberOfParticipants { get; set; }

		/// <summary>
		/// Deposit top-up amount
		/// </summary>
		public int depositTopUpAmount { get; set; }

		/// <summary>
		/// Circle of public bidding
		/// </summary>
		public int circle { get; set; }

		/// <summary>
		/// Status of public bidding id
		/// </summary>
		public Guid statusOfPublicBiddingId { get; set; }

		/// <summary>
		/// Best bidder id
		/// </summary>
		public Guid buyerId { get; set; }

		public BuyerDto buyer { get; set; }

	}
}
