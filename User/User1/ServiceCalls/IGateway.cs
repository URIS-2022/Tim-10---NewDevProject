using User1.Models;

namespace User1.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);

    }
}
