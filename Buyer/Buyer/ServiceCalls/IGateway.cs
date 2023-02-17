using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string servis);
    }
}
