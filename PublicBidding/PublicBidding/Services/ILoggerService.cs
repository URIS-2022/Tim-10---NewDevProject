using PublicBidding.Models;

namespace PublicBidding.Services
{
	public interface ILoggerService
	{
		/// <summary>
		/// Method for create message for logger
		/// </summary>
		/// <param name="message"></param>
		void CreateMessage(Message message);

	}
}
