namespace Parcel.Models
{
    public class AddressDto
    {
        public Guid addressId { get; set; }
        public string street { get; set; }
        public string place { get; set; }
        public string zipCode { get; set; }
    }
}
