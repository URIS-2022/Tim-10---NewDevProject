namespace DocumentAPI.Models
{
	public class Message
	{
		/// <summary>
		/// Service Name
		/// </summary>
		public string serviceName { get; set; }

		/// <summary>
		/// Method
		/// </summary>
		public string method { get; set; }

		/// <summary>
		/// Informations
		/// </summary>
		public string information { get; set; }

		/// <summary>
		/// Error
		/// </summary>
		public string error { get; set; }

	}
}
