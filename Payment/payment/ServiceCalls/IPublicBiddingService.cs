using payment.Models;

namespace payment.ServiceCalls
{
    public interface IPublicBiddingService
    {

        Task<PublicBiddingDto> GetPublicBiddings(Guid publicBiddingId);
    }
}
