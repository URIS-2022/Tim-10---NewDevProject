using AuthorizedPerson.Models;

namespace AuthorizedPerson.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string servis);
    }
}
