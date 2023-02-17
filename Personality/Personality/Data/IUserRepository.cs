namespace Personality.Data
{
    public interface IUserRepository
    {
        public bool UserWithCredentialsExists(string username, string password);
        bool validateUser(string username, string password);
    }
}
