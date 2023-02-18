using Buyer.Entities;

namespace Buyer.Data
{
    public class ContactPersonMockRepository : IContactPersonRepository
    {
        public static List<ContactPerson> contactPeople { get; set; } = new List<ContactPerson>();

        public ContactPersonMockRepository() {
            FillData();
        }
        public void FillData()
        {
            contactPeople.AddRange(new List<ContactPerson>
            {
                new ContactPerson
                {
                contactPersonId = Guid.Parse("E1ED563F-E902-4D84-92C9-AE1E066952A2"),
                name = "Amila",
                surname = "Salihbegovic",
                function = "function1",
                phoneNumber = "03245345654"
                },
                new ContactPerson
                {
                contactPersonId = Guid.Parse("65979E67-38D1-4B1F-B636-2D8C09DE25EA"),
                name = "Almir",
                surname = "Salihbegovic",
                function = "function2",
                phoneNumber = "02434354224"
                }
            });
        }
        public ContactPerson CreateContactPerson(ContactPerson contactPerson)
        {
            contactPerson.contactPersonId = Guid.NewGuid();
            contactPeople.Add(contactPerson);
            ContactPerson cp = GetContactPersonById(contactPerson.contactPersonId);

            return new ContactPerson
            {
                contactPersonId = cp.contactPersonId
            };
        }

        public void DeleteContactPerson(Guid cpid)
        {
            contactPeople.Remove(contactPeople.FirstOrDefault(cp => cp.contactPersonId == cpid));
        }

        public ContactPerson GetContactPersonById(Guid? cpid)
        {
            return contactPeople.FirstOrDefault(cp => cp.contactPersonId == cpid);
        }

        public List<ContactPerson> GetContactPesron()
        {
            return (from cp in contactPeople select cp).ToList();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public ContactPerson UpdateContactPerson(ContactPerson contactPerson)
        {
            ContactPerson cp = GetContactPersonById(contactPerson.contactPersonId);

            cp.contactPersonId = contactPerson.contactPersonId;
            cp.name= contactPerson.name;
            cp.surname= contactPerson.surname;
            cp.function= contactPerson.function;
            cp.phoneNumber = contactPerson.phoneNumber;

            return new ContactPerson
            {
                contactPersonId = cp.contactPersonId
            };

        }
    }
}
