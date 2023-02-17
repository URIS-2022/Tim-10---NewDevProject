namespace Parcel.Data
{
    public interface IUserRepository
    {
        public bool UserWithCredentialsExists(string userName, string password);
    }
}
