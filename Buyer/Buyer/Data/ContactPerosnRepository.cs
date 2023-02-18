using AutoMapper;
using Buyer.Entities;

namespace Buyer.Data
{
    public class ContactPerosnRepository : IContactPersonRepository
    {

        private readonly BuyerContext context;
        private readonly IMapper mapper;

        public ContactPerosnRepository(BuyerContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public ContactPerson CreateContactPerson(ContactPerson contactPerson)
        {
            contactPerson.contactPersonId = Guid.NewGuid();
            var NewEntity = context.contactPerson.Add(contactPerson);

            return mapper.Map<ContactPerson>(NewEntity.Entity);
        }

        public void DeleteContactPerson(Guid cpid)
        {
            ContactPerson contactPerson = GetContactPersonById(cpid);
            context.contactPerson.Remove(contactPerson);
        }

        public ContactPerson GetContactPersonById(Guid? cpid)
        {
            return context.contactPerson.FirstOrDefault(c => c.contactPersonId == cpid);
        }

        public List<ContactPerson> GetContactPesron()
        {
            return context.contactPerson.ToList();
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

        public ContactPerson UpdateContactPerson(ContactPerson contactPerson)
        {
            throw new NotImplementedException();
        }
    }
}
