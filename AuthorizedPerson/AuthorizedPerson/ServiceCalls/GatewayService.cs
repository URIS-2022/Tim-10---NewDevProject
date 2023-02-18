using AuthorizedPerson.Models;
using Newtonsoft.Json;

namespace AuthorizedPerson.ServiceCalls
{
    public class GatewayService : IGateway
    {
        private readonly IConfiguration configuration;

        public GatewayService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<GatewayDto> GetUrl(string servis)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{configuration["Services:GatewayService"]}{servis}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<GatewayDto>(responseContent);

                return gateway;
            }
        }
    }
}
