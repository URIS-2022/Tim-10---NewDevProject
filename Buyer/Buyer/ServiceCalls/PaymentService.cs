using Buyer.Models;
using Newtonsoft.Json;

namespace Buyer.ServiceCalls
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration configuration;

        public PaymentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<PaymentDto> GetPaymentById(Guid? payID)
        {

            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:Payment"]}api/payment/" + payID);
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
                    return JsonConvert.DeserializeObject<PaymentDto>(content);
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

