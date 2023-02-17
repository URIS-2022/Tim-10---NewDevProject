using Document.Models;

namespace Document.Services
{
	public interface IGateway
	{
		Task<GatewayDto> GetUrl(string service);

	}
}
