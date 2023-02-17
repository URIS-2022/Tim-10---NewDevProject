namespace Personality.Models
{
    public class PersonalityDto
    {
        /// <summary>
        /// Identifikator ličnosti
        /// </summary>
        public Guid personalityId { get; set; }

        /// <summary>
        /// Ime ličnosti
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Prezime ličnosti
        /// </summary>
        public string surname { get; set; }

        /// <summary>
        /// Funkija na kojoj je ličnost
        /// </summary>
        public string function { get; set; }
    }
}
