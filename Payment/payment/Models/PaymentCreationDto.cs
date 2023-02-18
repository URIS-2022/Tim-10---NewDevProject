using System.ComponentModel.DataAnnotations;

namespace payment.Models
{
    public class PaymentCreationDto
    {
        [Required(ErrorMessage = "Obavezno je uneti id.")]
        public Guid paymentId { get; set; }

        [Required(ErrorMessage = "Obavezno je uneti broj racuna.")]
        public string accountNumber { get; set; }

        
        [Required(ErrorMessage = "Obavezno je uneti poziv na broj.")]
        public string referenceNumber { get; set; }

        
        [Required(ErrorMessage = "Obavezno je uneti iznos.")]
        public float amount { get; set; }


        public string paymentPurpose { get; set; }

        
        public DateTime date { get; set; }

        
        public Guid exchangeRateId { get; set; }

        
        public Guid buyerId { get; set; }

        
        public Guid publicBiddingId { get; set; }




    }
}
