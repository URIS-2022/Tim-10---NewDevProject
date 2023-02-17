using complaint.Models;
using complaint.ServiceCalls;

using Newtonsoft.Json;

namespace complaint.ServiceCalls
{
    public class BuyerService : IBuyerService
    {
        private readonly IConfiguration configuration;

        public BuyerService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<BuyerDto> GetComplaintSubmitter(Guid buyerId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:BuyerService"]}api/buyer/" + buyerId);
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Accept", "application/json");
                var response = await httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrEmpty(content))
                    {
                        return default;
                    }
                    return JsonConvert.DeserializeObject<BuyerDto>(content);
                }
                return default;
            }
            catch
            {
                return default;
            }
        }
    }
}