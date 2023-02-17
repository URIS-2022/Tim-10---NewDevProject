using payment.Models;

namespace payment.ServiceCalls
{
    public interface IBuyerService
    {
       
            Task<BuyerDto> GetBuyer(Guid buyerId);
        

    }
}
