using AuthorizedPerson.Models;

namespace AuthorizedPerson.ServiceCalls
{
    public interface ILoggerService
    {
        void CreateMessage(Message message);
    }
}
