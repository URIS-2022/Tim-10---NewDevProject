using Country.Models;

namespace Country.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
