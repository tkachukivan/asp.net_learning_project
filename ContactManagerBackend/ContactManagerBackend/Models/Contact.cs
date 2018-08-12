using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactManagerBackend.Models
{
    public class Contact
    {
        public string Id { get; set; }
        public string OwnerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Birthdate { get; set; }
        public string MobilePhone { get; set; }
        public string HomePhone { get; set; }
    }
}
