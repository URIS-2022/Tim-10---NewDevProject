namespace PublicBidding.Models
{
	public class Message
	{
		/// <summary>
		/// Service name
		/// </summary>
		public string ServiceName { get; set; }

		/// <summary>
		/// Method
		/// </summary>
		public string Method { get; set; }

		/// <summary>
		/// Details
		/// </summary>
		public string Information { get; set; }

		/// <summary>
		/// Error
		/// </summary>
		public string Error { get; set; }

	}
}
