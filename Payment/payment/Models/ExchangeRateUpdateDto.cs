namespace payment.Models
{
    public class ExchangeRateUpdateDto
    {

        public Guid exchangeRateId { get; set; }
        public DateTime date { get; set; }  
        public string currency { get; set; }
        public float amount { get; set; }

    }
}
