﻿using Newtonsoft.Json;

namespace User1.ServiceCalls
{
    public class Gateway:IGateway
    {
        private readonly IConfiguration configuration;

        public Gateway(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<Models.GatewayDto> GetUrl(string service)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri url = new Uri($"{configuration["Services:Gateway"]}{service}");

                HttpResponseMessage response = client.GetAsync(url).Result;

                var responseContent = await response.Content.ReadAsStringAsync();
                var gateway = JsonConvert.DeserializeObject<Models.GatewayDto>(responseContent);

                return gateway;
            }
        }
    }
}
