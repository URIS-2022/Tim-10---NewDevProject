using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace AuthorizedPerson.Entities
{
    public class AuthorizedPersonModel
    {
        [Key]
        public Guid authorizedPersonId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string ducumentNumber { get; set; }
        public string tableNumber { get; set; }
        public Guid addressId { get; set; }

        override 
            public string ToString()
        {
            return "Authorized person: {authorizedPersonId: " + this.authorizedPersonId + ", name: " + this.name +
                ", surname: " + this.surname + ", document number: " + this.ducumentNumber + ", table number" + this.tableNumber +
                ", id address: " + this.addressId + " }";
        }
    }
}
