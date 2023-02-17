using System.ComponentModel.DataAnnotations;

namespace payment.Models
{
    public class ExchangeRateCreationDto
    {

        //izbacili smo id jer nam to ne treba prilikom kreiranja jer ce to baza napraviti sama

        /// <summary>
        /// Datum.
        /// </summary>
        public DateTime date { get; set; }

        /// <summary>
        /// Valuta.
        /// </summary>
        [Required(ErrorMessage = "Obavezno je uneti naziv valute.")]
        public string currency { get; set; }

        /// <summary>
        /// Vrednost.
        /// </summary>
        public float amount { get; set; }


    }
}
