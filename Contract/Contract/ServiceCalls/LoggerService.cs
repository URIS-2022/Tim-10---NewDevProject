using Contract.Models;
using Newtonsoft.Json;

namespace Contract.ServiceCalls
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
                    Uri url = new Uri($"{configuration["Services:LoggerService"]}api/logger");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(message));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage? response = client.PostAsync(url, content).Result;

                }
            }
            catch
            {
                //Empty catch
            }
        }
    }
}