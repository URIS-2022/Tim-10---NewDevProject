namespace Country.Entities
{
    /// <summary>
    /// Predstavlja model korisnika
    /// </summary>
    public class User
    {  
        /// <summary>
       /// Id korisnika
       /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ime korisnika
        /// </summary>
        public string? firstName { get; set; }

        /// <summary>
        /// Prezime korisnika
        /// </summary>
        public string? lastName { get; set; }

        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string? userName { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public string? email { get; set; }

        /// <summary>
        /// Hash-ovana šifra korisnika
        /// </summary>
        public string? password { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string? salt { get; set; }

    }
}
