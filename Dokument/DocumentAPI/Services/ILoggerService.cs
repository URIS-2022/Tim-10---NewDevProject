using DocumentAPI.Models;

namespace DocumentAPI.Services
{
	public interface ILoggerService
	{
		/// <summary>
		/// Method to create a message to the logger
		/// </summary>
		/// <param name="message"></param>
		void CreateMessage(Message message);

	}
}
