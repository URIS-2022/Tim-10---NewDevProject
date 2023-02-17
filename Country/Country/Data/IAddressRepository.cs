using Country.Entities;

namespace Country.Data
{
    public interface IAddressRepository
    {
        List<Address> GetAddressList();

        Address GetAddressById(Guid addressId);

        Address CreateAddress(Address address);

        void UpdateAddress(Address address);

        void DeleteAddress(Guid addressId);
        bool SaveChanges();
    }
}
