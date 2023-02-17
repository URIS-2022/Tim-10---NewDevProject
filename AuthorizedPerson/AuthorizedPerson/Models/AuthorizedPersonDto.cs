﻿using System.Globalization;

namespace AuthorizedPerson.Models
{
    public class AuthorizedPersonDto
    {
        public Guid authorizedPersonId { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string documentNumber { get; set; }
        public string tableNumber { get; set; }
        public Guid addressId { get; set; }



    }
}
