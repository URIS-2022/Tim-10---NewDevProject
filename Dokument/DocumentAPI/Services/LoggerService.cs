using DocumentAPI.Models;
using Newtonsoft.Json;

namespace DocumentAPI.Services
{
	public class LoggerService : ILoggerService
	{
		private readonly IConfiguration configuration;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="configuration"></param>
		public LoggerService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}

		/// <summary>
		/// Implementation of a method for creating a message to a logger
		/// </summary>
		/// <param name="message"></param>
		public void CreateMessage(Message message)
		{
			try
			{
				using (HttpClient client = new HttpClient())
				{
					var x = configuration["Services:LoggerService"];    //Services:LoggerService defined in appsettings.json and contains location of service
					Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

					HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
					content.Headers.ContentType.MediaType = "application/json";

					HttpResponseMessage response = client.PostAsync(url, content).Result;

				}
			}
			catch
			{

			}
		}

	}
}
