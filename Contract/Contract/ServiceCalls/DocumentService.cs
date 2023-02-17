using Contract.Models;
using Newtonsoft.Json;

namespace Contract.ServiceCalls
{
    public class DocumentService : IDocumentService
    {
        private readonly IConfiguration configuration;

        public DocumentService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }


        public async Task<DocumentDto> GetDocumentById(Guid documentId)
        {
            try
            {
                using var httpClient = new HttpClient();
                Uri url = new Uri($"{configuration["Services:DocumentService"]}api/document/" + documentId);
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
                    return JsonConvert.DeserializeObject<DocumentDto>(content);
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
