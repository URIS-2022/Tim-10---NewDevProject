using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBidding.Entities
{
	/// <summary>
	/// Public bidding entity
	/// </summary>
	public class PublicBidding
	{
		/// <summary>
		/// Public bidding id
		/// </summary>
		[Key]
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
		[ForeignKey("typeOfPublicBidding")]
		public Guid typePublicBiddingId { get; set; }
		public TypeOfPublicBidding typeOfPublicBidding { get; set; }

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
		[ForeignKey("statusOfPublicBidding")]
		public Guid statusOfPublicBiddingId { get; set; }
		public StatusOfPublicBidding statusOfPublicBidding { get; set; }

		/// <summary>
		/// Id of the address of the public bidding
		/// </summary>
		public Guid addressId { get; set; }

		/// <summary>
		/// Id of the user 
		/// </summary>
		public Guid userId { get; set; }

		/// <summary>
		/// Id of the authorized bidder person
		/// </summary>
		public Guid authorizedBidderPersonId { get; set; }

		/// <summary>
		/// List of parcels id
		/// </summary>
		[NotMapped]
		public List<Guid> parcelsId { get; set; }

		/// <summary>
		/// Best buyer id
		/// </summary>
		public Guid buyerId { get; set; }

		/// <summary>
		/// List of registered customer Id
		/// </summary>
		[NotMapped]
		public List<Guid> buyersId { get; set; }

	}
}
