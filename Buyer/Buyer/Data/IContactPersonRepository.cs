using Buyer.Entities;

namespace Buyer.Data
{
    public interface IContactPersonRepository
    {
        List<ContactPerson> GetContactPesron();
        ContactPerson GetContactPersonById(Guid? cpid);
        ContactPerson CreateContactPerson(ContactPerson contactPerson);
        ContactPerson UpdateContactPerson(ContactPerson contactPerson);
        void DeleteContactPerson(Guid cpid);
        bool SaveChanges();
    }
}
