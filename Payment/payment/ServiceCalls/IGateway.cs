using payment.Models;
namespace payment.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
