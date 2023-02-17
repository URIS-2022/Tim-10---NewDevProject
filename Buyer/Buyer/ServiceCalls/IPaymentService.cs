using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface IPaymentService
    {
        public Task<PaymentDto> GetPaymentById(Guid? payID);
    }
}
