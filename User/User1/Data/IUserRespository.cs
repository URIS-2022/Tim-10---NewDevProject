using User1.Entities;

namespace User1.Data
{
    public interface IUserRespository
    {
        List<User> GetUserList();
        User GetUserById(Guid userId); //vraca 1 prijavu po id-u
        User CreateUser(User user); //kreiranje korisnika
        void UpdateUser(User user); //update korisnika
        void DeleteUser(Guid userId); //brisanje
        bool SaveChanges();
        public bool UserWithCredentialsExists(string username, string password);
    }
}
