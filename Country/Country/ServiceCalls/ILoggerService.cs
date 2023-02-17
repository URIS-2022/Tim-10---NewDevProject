using Country.Models;

namespace Country.ServiceCalls
{
    public interface ILoggerService
    {
        /// <summary>
        /// Metoda za kreiranje poruke logeru
        /// </summary>
        /// <param name="message"></param>
        void CreateMessage(Message message);
    }
}
