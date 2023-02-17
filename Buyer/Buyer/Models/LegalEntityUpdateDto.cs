using System.ComponentModel.DataAnnotations.Schema;

namespace Buyer.Models
{
    public class LegalEntityUpdateDto
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
        public string addressId { get; set; }
        public string paymentId { get; set; }
        public string publicBiddingId { get; set; }
        public string legalEntityName { get; set; }
        public string legalEntityId { get; set; }
        public string legalEntityFax { get; set; }
        public Guid contactPerson { get; set; }

        [ForeignKey("PriorityModel")]
        public Guid priorityId { get; set; }

        public AuthorizedPersonDto authorizedPersonDto { get; set; }
        public PaymentDto paymentDto { get; set; }
        public AddressDto addressDto { get; set; }
        public PublicBiddingDto publicBiddingDto { get; set; }
    }
}
