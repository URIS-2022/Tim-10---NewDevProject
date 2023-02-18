using Newtonsoft.Json;
using PublicBidding.Models;

namespace PublicBidding.Services
{
	public class LoggerService : ILoggerService
	{
		private readonly IConfiguration configuration;


		/// <summary>
		/// Konstruktor
		/// </summary>
		/// <param name="configuration"></param>
		public LoggerService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		/// <summary>
		/// Create message
		/// </summary>
		/// <param name="message"></param>
		public void CreateMessage(Message message)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

					HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
					content.Headers.ContentType.MediaType = "application/json";

				}
			}
			catch
			{
				// error if can't find url
			}


		}
	}
}
