using payment.Models;
using Newtonsoft.Json;
using payment.ServiceCalls;

namespace payment.ServiceCalls
{
    public class PublicBiddingService : IPublicBiddingService
    {
        private readonly IConfiguration configuration;

        public PublicBiddingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<PublicBiddingDto> GetPublicBiddings(Guid publicBiddingId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:PublicBidding"]}api/publicBidding/" + publicBiddingId);
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
                    return JsonConvert.DeserializeObject<PublicBiddingDto>(content);
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
