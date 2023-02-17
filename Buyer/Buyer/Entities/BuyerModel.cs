using Buyer.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Buyer.Entities
{
    public class BuyerModel
    {
        [Key]
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
        public Guid priorityId { get; set; }
        [NotMapped]
        public AuthorizedPersonDto authorizedPersonDto { get; set; }
        [NotMapped]
        public PaymentDto paymentDto { get; set; }
        [NotMapped]
        public AddressDto addressDto { get; set; }
        [NotMapped]
        public PublicBiddingDto publicBiddingDto { get; set; }

        override
            public string ToString()
        {
            return "Buyer: {BuyerID: " + this.buyerId + ", Buyer type: " + this.buyerType + ", Area: " + this.area + ", Ban: " + this.ban
                + ", Ban Starting Date: " + this.banStartingDate + ", Ban Lasting " + this.banLasting + ", Ban Ending Date: " + this.banEndingDate +
                ", Authorized Person: " + this.authorizedPersonId + ", PriorityID: " + this.priorityId + ", Phone number 1: " + this.phoneNumber1 + ", Phone number 2: " + this.phoneNumber2 +
                ", Email address: " + this.emailAddress + ", Account number: " + this.accountNumber +", Payment: "+ this.paymentId + ", Address ID: " + this.addressId +", public biddingID: "+this.publicBiddingId+ ", }";
        }
    }
}
