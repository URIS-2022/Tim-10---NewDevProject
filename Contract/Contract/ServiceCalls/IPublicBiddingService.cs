using Contract.Models;

namespace Contract.ServiceCalls
{
    public interface IPublicBiddingService
    {
        public Task<PublicBiddingDto> GetPublicBiddingById(Guid publicBiddingId);
    }
}
