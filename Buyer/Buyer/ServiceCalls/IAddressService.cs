using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface IAddressService
    {
        public Task<AddressDto> GetAddressById(Guid aID);
    }
}
