using System.ComponentModel.DataAnnotations;

namespace Country.Entities
{
    public class Address
    {
        [Key]
        public Guid addressId { get; set; }
        public string? street { get; set; } 
        public string? place { get; set; }
        public int zipCode { get; set; }
        override
        public string ToString()
        {
            return "Address: { Id:" + this.addressId + " ";
        }
    }
}
