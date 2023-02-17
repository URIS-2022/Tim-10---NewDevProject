using PublicBidding.Models;

namespace PublicBidding.Services
{
	public interface IGateway
	{
		Task<GatewayDto> GetUrl(string service);

	}
}
