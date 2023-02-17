namespace Personality.Entities
{
    public class User
    {
        /// <summary>
        /// ID korisnika.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Ime korisnika.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Prezime korisnika.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Korisnicko ime.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email korisnika.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hash-ovana sifra korisnika.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt.
        /// </summary>
        public string Salt { get; set; }

        internal static User FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}

