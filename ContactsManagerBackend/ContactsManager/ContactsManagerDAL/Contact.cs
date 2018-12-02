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
        public DateTime Birthdate { get; set; }

        public void LoadDataFromReader(SqlDataReader reader)
        {
            Id = Guid.Parse(reader["Id"].ToString());
            FirstName = reader["FirstName"].ToString();
            LastName = reader["LastName"].ToString();
            Email = reader["Email"].ToString();
            Birthdate = Convert.ToDateTime(reader["Birthdate"].ToString());
        }
    }
}
