namespace PublicBidding.Entities
{
	/// <summary>
	/// User entity
	/// </summary>
	public class User
	{
		public Guid id { get; set; } = default!;
		public string firstName { get; set; } = default!;
		public string lastName { get; set; } = default!; 
		public string userName { get; set; } = default!;	
		public string email { get; set; } = default!;
		public string password { get; set; } = default!;
		public string salt { get; set; } = default!;

	}
}
