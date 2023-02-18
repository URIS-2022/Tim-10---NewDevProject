using Azure;
using Commission.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Commission.ServiceCalls
{
    public class PersonalityService : IPersonalityService
    {
        private readonly IConfiguration configuration;

        public PersonalityService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public async Task<PersonalityDto> GetPersonality(Guid personalityId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:Personality"]}api/personality/" + personalityId);
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
                    return JsonConvert.DeserializeObject<PersonalityDto>(content);
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
