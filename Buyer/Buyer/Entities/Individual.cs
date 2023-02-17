namespace Buyer.Entities
{
    public class Individual:BuyerModel
    {
        public Individual()
        {
        }
        public Individual(BuyerModel buyer)
        {
            buyerId = buyer.buyerId;
            buyerType = buyer.buyerType;
            area = buyer.area;
            ban = buyer.ban;
            banStartingDate = buyer.banStartingDate;
            banLasting = buyer.banLasting;
            banEndingDate = buyer.banEndingDate;
            authorizedPersonId = buyer.authorizedPersonId;
            priorityId = buyer.priorityId;
            phoneNumber1 = buyer.phoneNumber1;
            phoneNumber2 = buyer.phoneNumber2;
            emailAddress = buyer.emailAddress;
            addressId = buyer.addressId;
            paymentId = buyer.paymentId;
            publicBiddingId = buyer.publicBiddingId;
            accountNumber = buyer.accountNumber;
        }
        public string individualName { get; set; }
        public string individualSurname { get; set; }
        public string individualId { get; set; }
        
        override
           public string ToString()
        {
            return "Buyer: {BuyerID: " + this.buyerId + ", Buyer type: " + this.buyerType + ", Area: " + this.area + ", Ban: " + this.ban
                + ", Ban Starting Date: " + this.banStartingDate + ", Ban Lasting " + this.banLasting + ", Ban Ending Date: " + this.banEndingDate +
                ", Authorized Person: " + this.authorizedPersonId + ", PriorityID: " + this.priorityId + ", Phone number 1: " + this.phoneNumber1 + ", Phone number 2: " + this.phoneNumber2 +
                ", Email address: " + this.emailAddress + ", Account number: " + this.accountNumber + ", Individial ID: " + this.individualId+", Individual name: "+ this.individualName+
                ", Individual surname: "+this.individualSurname + ", Payment: " + this.paymentId + ", Address ID: " + this.addressId + ", public biddingID: " + this.publicBiddingId + ", }";
        }
        
    }
}
