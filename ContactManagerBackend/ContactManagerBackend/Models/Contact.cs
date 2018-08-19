using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerBackend.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }
        public Enums.PhoneType Type { get; set; }
    }

    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Appartments { get; set; }
        public string ZipCode { get; set; }
    }

    public class Contact
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }
        public DateTime Birthdate { get; set; }
        public List<Phone> Phones { get; set; }
    }
}
