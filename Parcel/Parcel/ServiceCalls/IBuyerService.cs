using Parcel.Models;

namespace Parcel.ServiceCalls
{
    public interface IBuyerService
    {
        public Task<BuyerDto> GetBuyerById(Guid buyerId);
    }
}
