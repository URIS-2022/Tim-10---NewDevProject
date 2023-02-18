using System.ComponentModel.DataAnnotations;

namespace payment.Entities
{
    public class ExchangeRate
    {

        [Key]
        public Guid exchangeRateId { get; set; }
        public DateTime date { get; set; }
        public string? currency { get; set; }
        public float value { get; set; }


    }
}
