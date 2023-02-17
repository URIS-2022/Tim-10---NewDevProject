namespace AuthorizedPerson.Entities
{
    public class User
    {
        /// <summary>
        /// Id user
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User surname
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// username
        /// </summary>
        public string UserName { get; set; }

        ///// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Salt
        /// </summary>
        public string Salt { get; set; }
    }
}
