using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ContactsManagerDAL
{
    public class ContactsProvider
    {
        public List<Contact> GetContacts()
        {
            List<Contact> Contacts = new List<Contact>();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContacts";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var contact = new Contact();

                        contact.Id = Guid.Parse(reader["Id"].ToString());
                        contact.FirstName = reader["FirstName"].ToString();
                        contact.LastName = reader["LastName"].ToString();
                        contact.Email = reader["Email"].ToString();
                        contact.Birthdate = Convert.ToDateTime(reader["Birthdate"].ToString());


                        Contacts.Add(contact);
                    }
                }
            }

            return Contacts;
        }
    }
}
