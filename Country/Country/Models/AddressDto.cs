namespace Country.Models
{
    public class AddressDto
    {
        public Guid addressId { get; set; }
        public string? street { get; set; }
        public string? place { get; set; }
        public int zipCode { get; set; }
    }
}
