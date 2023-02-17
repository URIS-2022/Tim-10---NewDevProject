using System.ComponentModel.DataAnnotations.Schema;

namespace PublicBidding.Models
{
	public class BuyerDto
	{
		public Guid buyerId { get; set; }
		public bool buyerType { get; set; }
		public string area { get; set; }
		public bool ban { get; set; }
		public DateTime banStartingDate { get; set; }
		public string banLasting { get; set; }
		public DateTime banEndingDate { get; set; }
		public Guid? authorizedPersonId { get; set; }
		//Individual and Legal 
		public string phoneNumber1 { get; set; }
		public string phoneNumber2 { get; set; }
		public string emailAddress { get; set; }
		public string accountNumber { get; set; }
		public Guid addressId { get; set; }
		public Guid paymentId { get; set; }
		public string publicBiddingId { get; set; }
		public Guid priorityId { get; set; }
	}
}
