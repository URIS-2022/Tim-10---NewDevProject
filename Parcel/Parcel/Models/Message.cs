namespace Parcel.Models
{
    public class Message
    {
        /// <summary>
        /// Naziv servisa
        /// </summary>
        public string serviceName { get; set; }

        /// <summary>
        /// Metoda
        /// </summary>
        public string method { get; set; }

        /// <summary>
        /// Detalji
        /// </summary>
        public string information { get; set; }

        /// <summary>
        /// Greska
        /// </summary>
        public string error { get; set; }
    }

}
