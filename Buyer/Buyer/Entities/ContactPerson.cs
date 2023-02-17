namespace Buyer.Entities
{
    public class ContactPerson
    {
        public Guid contactPersonId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string function { get; set; }
        public string phoneNumber { get; set; }

        override
            public string ToString()
        {
            return "Contact person: {Contact person ID: " + this.contactPersonId + ", Name: " + this.name +
                ", Surname: " + this.surname + ", function: " + this.function + ", phone number: " + this.phoneNumber + ", }";
        }
    }
}
