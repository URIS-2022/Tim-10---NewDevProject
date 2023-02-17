using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface IPublicBiddingService 
    {
        public Task<PublicBiddingDto> GetPublicBidding(Guid PBID);
    }
}
