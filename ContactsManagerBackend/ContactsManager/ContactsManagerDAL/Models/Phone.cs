using System;
using System.Data.SqlClient;
using ContactsManagerDAL.Enums;
namespace ContactsManagerDAL.Models
{
    public class Phone
    {
        public Guid Id { get; set; }
        public PhoneNumber Number { get; set; }
        public PhoneType PhoneType { get; set; }

        public void LoadDataFromReader(SqlDataReader reader)
        {
            Id = (Guid)reader["Id"];
            PhoneType = (PhoneType)(int)reader["PhoneType"];
            Number = new PhoneNumber(
                (string)reader["CountryCode"],
                (string)reader["PhoneNumber"]
                ); 
        }
    }
}
