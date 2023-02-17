using Buyer.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Buyer.ServiceCalls
{
    public class PublicBiddingService : IPublicBiddingService
    {
        private readonly IConfiguration configuration;
        public PublicBiddingService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        ///<summary>
        ///
        /// </summary>
        /// <param name="PBID"></param>
        /// <returns></returns>
        public async Task<PublicBiddingDto> GetPublicBidding(Guid PBID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri uri = new Uri($"{configuration["Services: Bidding"]}api/publicBidding/" + PBID);
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
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
