using complaint.Models;
namespace complaint.ServiceCalls
{
    public interface IGateway
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
