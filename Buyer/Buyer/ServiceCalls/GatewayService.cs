using Buyer.Models;
using Newtonsoft.Json;

namespace Buyer.ServiceCalls
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
                var x = configuration["Services:GatewayService"];
                Uri url = new Uri($"{configuration["Services:GatewayService"]}{servis}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<GatewayDto>(responseContent);

                return gateway;
            }
        }
    }
}
