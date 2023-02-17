using Contract.Models;

namespace Contract.ServiceCalls
{
    public interface IBuyerService
    {
        public Task<BuyerDto> GetBuyerById(Guid buyerId);
    }
}
