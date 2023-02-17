namespace DocumentAPI.Models
{
	public class UserDto
	{
		/// <summary>
		/// ID of user
		/// </summary>
		public Guid userId { get; set; }

		/// <summary>
		/// ID of user type
		/// </summary>
		public Guid typeOfUserId { get; set; }

		/// <summary>
		/// firstName
		/// </summary>
		public string firstName { get; set; }

		/// <summary>
		/// lastName
		/// </summary>
		public string lastName { get; set; }

		/// <summary>
		/// username
		/// </summary>
		public string username { get; set; }

		/// <summary>
		/// password
		/// </summary>
		public string password { get; set; }

	}
}
