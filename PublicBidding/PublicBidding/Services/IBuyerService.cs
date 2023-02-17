using PublicBidding.Models;

namespace PublicBidding.Services
{
	public interface IBuyerService
	{
		Task<BuyerDto> GetBestBidder(Guid buyerId);

	}
}
