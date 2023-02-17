using Contract.Models;

namespace Contract.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
