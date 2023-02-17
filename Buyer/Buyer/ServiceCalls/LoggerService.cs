using Buyer.Models;
using Newtonsoft.Json;

namespace Buyer.ServiceCalls
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
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var x = configuration["Services:LoggerService"];    //Services:LoggerService is defined in appsettings.json 
                    Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = client.PostAsync(url, content).Result;

                }
            }
            catch
            {
                //stop crashing
            }
        }
    }
}
