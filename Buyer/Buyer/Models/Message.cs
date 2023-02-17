namespace Buyer.Models
{
    public class Message
    {
        /// <summary>
        /// service name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// method
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// information
        /// </summary>
        public string Information { get; set; }

        /// <summary>
        /// error
        /// </summary>
        public string Error { get; set; }
    }
}
