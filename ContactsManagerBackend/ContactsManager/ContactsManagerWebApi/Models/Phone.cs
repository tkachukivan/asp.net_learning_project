using System;

namespace ContactsManagerWebApi.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public PhoneNumber Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public bool IsNew { get; set; }
        public bool Deleted { get; set; }
    }

    public struct PhoneNumber
    {
        public string CountryCode;
        public string Number;
    }

    public enum PhoneType
    {
        Home,
        Mobile,
        Other
    }
}
