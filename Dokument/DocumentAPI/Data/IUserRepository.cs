namespace DocumentAPI.Data
{
	public interface IUserRepository
	{
		public bool UserWithCredentialsExists(string username, string password);

	}
}
