using AutoMapper;
using User1.Entities;

namespace User1.Data
{
    public class UserTypeRepository : IUserTypeRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public UserTypeRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public List<UserType> GetUserTypeList()
        {
            return context.UserType.ToList();
        }
        public UserType GetUserTypeId(Guid userTypeId)
        {
            return context.UserType.FirstOrDefault(e => e.userTypeId == userTypeId);
        }
        public UserType CreateUserType(UserType userType)
        {
            var createdEntity = context.Add(userType);
            return mapper.Map<UserType>(createdEntity.Entity);
        }

        public void UpdateUserType(UserType userType)
        {
            //Nije potrebna implementacija jer EF core prati entitet koji smo izvukli iz baze
            //i kada promenimo taj objekat i odradimo SaveChanges sve izmene će biti perzistirane
        }
        public void DeleteUserType(Guid userTypeId)
        {
            var userType = GetUserTypeId(userTypeId);
            context.Remove(userType);
        }

        public UserType GetUserTypeById(Guid userTypeId)
        {
            throw new NotImplementedException();
        }
    }
}
