using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ContactsManagerDAL
{
    public class ContactsProvider
    {
        public List<Contact> GetContacts()
        {
            List<Contact> Contacts = new List<Contact>();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"getContacts";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var c = new Contact();

                            c.Id = reader.GetGuid(0);
                            c.FirstName = reader.GetString(1);
                            c.LastName = reader.GetString(2);
                            c.Email = reader.GetString(3);
                            c.Birthdate = reader.GetDateTime(4);

                            Contacts.Add(c);
                        }
                    }
                }
            }

            return Contacts;
        }
    }
}
