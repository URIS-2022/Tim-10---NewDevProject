using AutoMapper;
using System.Security.Cryptography;
using User1.Entities;

namespace User1.Data
{
    public class UserRepository : IUserRespository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;


        public UserRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;


        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public List<User> GetUserList()
        {
            return context.User.ToList();
        }
        public User GetUserById(Guid userId)
        {
            return context.User.FirstOrDefault(e => e.userId == userId);
        }

        public User CreateUser(User user)
        {
            user.userId = Guid.NewGuid();

            var password = HashPassword(user.password);

            user.password = password.Item1;
            user.salt = password.Item2;

            var createdEntity = context.Add(user);
            return mapper.Map<User>(createdEntity.Entity);
        }

      

        public void DeleteUser(Guid userId)
        {
            var user = GetUserById(userId);
            context.Remove(user);
        }

        private static Tuple<string, string> HashPassword(string password)
        {
            var sBytes = new byte[password.Length];

            new RNGCryptoServiceProvider().GetNonZeroBytes(sBytes);

            var salt = Convert.ToBase64String(sBytes);
            var derivedBytes = new Rfc2898DeriveBytes(password, sBytes, 100);

            return new Tuple<string, string>
            (
                Convert.ToBase64String(derivedBytes.GetBytes(256)),
                salt
            );
        }


        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool UserWithCredentialsExists(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
