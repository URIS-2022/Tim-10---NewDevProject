using User1.Entities;

namespace User1.Data
{
    public interface IUserTypeRepository
    {
        List<UserType> GetUserTypeList();
        UserType GetUserTypeById(Guid userTypeId);
        UserType CreateUserType(UserType userType);
        void UpdateUserType(UserType userType);
        void DeleteUserType(Guid userTypeId);
        bool SaveChanges();
    }
}
