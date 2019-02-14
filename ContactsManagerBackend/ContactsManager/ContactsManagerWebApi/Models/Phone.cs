using System;
using ContactsManagerBL.Enums;

namespace ContactsManagerWebApi.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public PhoneNumber LocalNumber { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
