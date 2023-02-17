using Newtonsoft.Json;
using Personality.Models;

namespace Personality.ServiceCalls
{
    public class UserService: IUserService
    {
        private readonly IConfiguration configuration;

        public UserService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public bool validateUser(Principal principal)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var x = configuration["Services:UserService"];
                    Uri url = new Uri($"{configuration["Services:UserService"]}api/korisnici");

                    HttpContent content = new StringContent(JsonConvert.SerializeObject(principal));
                    content.Headers.ContentType.MediaType = "application/json";

                    HttpResponseMessage response = client.PostAsync(url, content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
