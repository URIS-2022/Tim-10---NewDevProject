using Contract.Models;

namespace Contract.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
