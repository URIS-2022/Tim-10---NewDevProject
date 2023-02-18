using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBidding.Models
{
	public class BuyerDto
	{
		public Guid buyerId { get; set; } = default!;
		public bool buyerType { get; set; } = default!;
		public string area { get; set; } = default!;
		public bool ban { get; set; } = default!;
		public DateTime banStartingDate { get; set; } = default!;
		public string banLasting { get; set; } = default!;
		public DateTime banEndingDate { get; set; } = default!;
		public Guid? authorizedPersonId { get; set; } = default!;
		//Individual and Legal 
		public string phoneNumber1 { get; set; } = default!;
		public string phoneNumber2 { get; set; } = default!;
		public string emailAddress { get; set; } = default!;
		public string accountNumber { get; set; } = default!;
		public Guid addressId { get; set; } = default!;
		public Guid paymentId { get; set; } = default!;
		public string publicBiddingId { get; set; } = default!;
		public Guid priorityId { get; set; } = default!;
	}
}
