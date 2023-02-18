using AuthorizedPerson.Models;
using Newtonsoft.Json;

namespace AuthorizedPerson.ServiceCalls
{
    public class LoggerService : ILoggerService
    {
        private readonly IConfiguration configuration;

        public LoggerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void CreateMessage(Message message)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                   //Services:LoggerService is defined in appsettings.json 
                    Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                }
                catch
                {
                    //stop crashing
                }
            }
        }
    }
}
