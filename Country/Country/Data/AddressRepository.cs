using AutoMapper;
using Country.Entities;

namespace Country.Data
{
    public class AddressRepository : IAddressRepository
    {

        private readonly CountryContext context;
        private readonly IMapper mapper;

        public AddressRepository(CountryContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }
        public Address CreateAddress(Address address)
        {
            var createdEntity = context.Add(address);
            context.SaveChanges();
            return mapper.Map<Address>(createdEntity.Entity);
        }

        public void DeleteAddress(Guid addressId)
        {
            var address = GetAddressById(addressId);
            context.Remove(address);
            context.SaveChanges();
        }

        public Address GetAddressById(Guid addressId)
        {
            return context.Address.FirstOrDefault(a => a.addressId == addressId);
        }

        public List<Address> GetAddressList()
        {
            return context.Address.ToList();
        }

        public void UpdateAddress(Address address)
        {
            //nije potrebno implementirati posebno update
        }
    }
}
