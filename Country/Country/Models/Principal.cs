namespace Country.Models
{
    /// <summary>
    /// Predstavlja model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string? userName { get; set; }

        /// <summary>
        /// Korisnička lozinka
        /// </summary>
        public string? password { get; set; }
    }
}
