using AuthorizedPerson.Entities;

namespace AuthorizedPerson.Data
{
    public interface IAuthorizedPersonRepository
    {
        List<AuthorizedPersonModel> GetAuthorizedPeople();

        AuthorizedPersonModel GetAuthorizedPersonById(Guid APID);
        AuthorizedPersonModel CreateAuthorizedPerson(AuthorizedPersonModel authorizedPerson);
        AuthorizedPersonModel UpdateAuthorizedPerson(AuthorizedPersonModel authorizedPerson);
        void DeleteAuthorizedPerson(Guid APID);
        bool SaveChanges();
    }
}
