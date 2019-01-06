using System;
using System.Data.SqlClient;

namespace ContactsManagerDAL.Models
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

        public void LoadDataFromReader(SqlDataReader reader)
        {
            Id = (Guid)reader["AddressId"];
            Country = (string)reader["Country"];
            City = (string)reader["City"];
            Street = (string)reader["Street"];
            Building = reader["Building"].GetType() != typeof(DBNull) ? (string)reader["Building"] : "";
            Appartment = reader["Appartment"].GetType() != typeof(DBNull) ? (string)reader["Appartment"] : "";
            ZipCode = reader["ZipCode"].GetType() != typeof(DBNull) ? (string)reader["ZipCode"] : "";
        }
    }
}
