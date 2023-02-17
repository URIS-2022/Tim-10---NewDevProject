namespace payment.Models
{
    public class PaymentDto
    {


        public Guid paymentId { get; set; }
        public string accountNumber { get; set; }

        
        public string referenceNumber { get; set; }

        
        public float amount { get; set; }

        
        public string paymentPurpose { get; set; }

       
        public DateTime date { get; set; }

        
        public Guid exchangeRateId { get; set; }

      
        public Guid publicBiddingId { get; set; }

        public PublicBiddingDto publicBidding{ get; set; }

        
        public Guid buyerId { get; set; }

        public BuyerDto buyer { get; set; }




    }
}
