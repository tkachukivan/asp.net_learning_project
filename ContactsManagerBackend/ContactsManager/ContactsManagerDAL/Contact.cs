using System;
using System.Data.SqlClient;

namespace ContactsManagerDAL
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }

        public void LoadDataFromReader(SqlDataReader reader)
        {
            Id = (Guid)reader["Id"];
            FirstName = (string)reader["FirstName"];
            LastName = (string)reader["LastName"];
            Email = (string)reader["Email"];

            var col = reader.GetOrdinal("Birthdate");

            Birthdate = reader.IsDBNull(col) ?
                        (DateTime?)null :
                        (DateTime?)reader["Birthdate"];
        }
    }
}
