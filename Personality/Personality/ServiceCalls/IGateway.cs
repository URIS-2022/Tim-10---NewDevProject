using Personality.Models;

namespace Personality.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);

    }
}
