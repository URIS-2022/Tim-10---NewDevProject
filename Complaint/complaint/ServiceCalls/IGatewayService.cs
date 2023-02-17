using complaint.Models;
namespace complaint.ServiceCalls
{
    public interface IGatewayService
    {
        Task<GatewayDto> GetUrl(string service);
    }
}
