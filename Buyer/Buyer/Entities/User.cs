namespace Buyer.Entities
{
    /// <summary>
    /// User model
    /// </summary>
    public class User
    {

        /// <summary>
        /// User id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// First name 
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///  Last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///  Username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Hashed password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
    }
}
