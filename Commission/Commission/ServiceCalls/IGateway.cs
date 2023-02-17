using Commission.Models;

namespace Commission.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
