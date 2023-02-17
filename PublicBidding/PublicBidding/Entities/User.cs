namespace PublicBidding.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public class User
	{
		public Guid id { get; set; }
		public string firstName { get; set; }
		public string lastName { get; set; }
		public string userName { get; set; }
		public string email { get; set; }
		public string password { get; set; }
		public string salt { get; set; }

	}
}
