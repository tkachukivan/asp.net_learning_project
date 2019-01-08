using System;
using System.Data.SqlClient;

namespace ContactsManagerDAL.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public PhoneNumber Number { get; set; }
        public PhoneType PhoneType { get; set; }
        public bool IsNew { get; set; }
        public bool Deleted { get; set; }

        public void LoadDataFromReader(SqlDataReader reader)
        {
            Id = (Guid)reader["PhoneId"];
            PhoneType = (PhoneType)(int)reader["PhoneType"];
            Number = new PhoneNumber(
                (string)reader["CountryCode"],
                (string)reader["PhoneNumber"]
                ); 
        }
    }

    public struct PhoneNumber
    {
        public string CountryCode;
        public string Number;

        public PhoneNumber (string countryCode, string number)
        {
            CountryCode = countryCode;
            Number = number;
        }
    }

    public enum PhoneType
    {
        Home,
        Mobile,
        Other
    }
}
