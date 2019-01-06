using System;

namespace ContactsManagerWebApi.Models
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Building { get; set; }
        public string Appartment { get; set; }
        public string ZipCode { get; set; }
    }
}
