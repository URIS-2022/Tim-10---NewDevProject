using Buyer.Models;
using Newtonsoft.Json;

namespace Buyer.ServiceCalls
{
    public class AuthorizedPersonService : IAuthorizedPersonService
    {
        private readonly IConfiguration configuration;

        public AuthorizedPersonService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<AuthorizedPersonDto> GetAuthorizedPersonById(Guid? APID)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:AuthorizedPerson"]}api/authorizedPerson/" + APID);
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
                    return JsonConvert.DeserializeObject<AuthorizedPersonDto>(content);
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
