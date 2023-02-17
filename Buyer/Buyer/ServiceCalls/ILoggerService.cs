using Buyer.Models;

namespace Buyer.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
