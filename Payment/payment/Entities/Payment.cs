using System.ComponentModel.DataAnnotations.Schema;

namespace payment.Entities
{
    public class Payment
    {
        public Guid paymentId { get; set; }
        public string? accountNumber { get; set; }
        public string? referenceNumber { get; set; }
        public float amount { get; set; }
        public string? paymentPurpose { get; set; }
        public DateTime date { get; set; }

        [ForeignKey("ExchangeRate")]
        public Guid exchangeRateId { get; set; }
        public ExchangeRate? ExchangeRate { get; set; }

        public Guid buyerId { get; set; }
        public Guid publicBiddingId { get; set; }


    }
}
