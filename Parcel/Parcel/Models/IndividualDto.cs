namespace Parcel.Models
{
    public class IndividualDto
    {
        public Guid buyerId { get; set; }
        public string individualName { get; set; }
        public string identificationNumber { get; set; }
        public AddressDto Address { get; set; }
        public string phoneNumber1 { get; set; }
        public string phoneNumber2 { get; set; }
        public string Fax { get; set; }
        public string emailAddress { get; set; }
    }
}
