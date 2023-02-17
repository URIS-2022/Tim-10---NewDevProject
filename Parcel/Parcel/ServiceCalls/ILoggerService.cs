using Parcel.Models;

namespace Parcel.ServiceCalls
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
