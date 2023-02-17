using Buyer.Models;
using Newtonsoft.Json;

namespace Buyer.ServiceCalls
{
    public class AddressService : IAddressService
    {
        private readonly IConfiguration configuration;

        public AddressService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aID"></param>
        /// <returns></returns>
        public async Task<AddressDto> GetAddressById(Guid aID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:Place"]}api/address/" + aID);
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
                    return JsonConvert.DeserializeObject<AddressDto>(content);
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
