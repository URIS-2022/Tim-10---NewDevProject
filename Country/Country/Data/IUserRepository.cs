namespace Country.Data
{
    public interface IUserRepository
    {
        public bool UserWithCredentialsExists(string userName, string password);
    }
}
