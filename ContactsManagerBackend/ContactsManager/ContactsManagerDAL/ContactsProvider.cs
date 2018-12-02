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

                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var contact = new Contact();

                        contact.LoadDataFromReader(reader);

                        Contacts.Add(contact);
                    }
                }
            }

            return Contacts;
        }

        public Contact GetContactById(Guid Id)
        {
            var contact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContactById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var idParameter = new SqlParameter("Id", System.Data.SqlDbType.UniqueIdentifier);
                idParameter.Value = Id;
                cmd.Parameters.Add(idParameter);

                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    contact.LoadDataFromReader(reader);
                }
                else
                {
                    contact = null;
                }
            }

            return contact;
        }

        public Contact CreateContact(Contact contact)
        {
            var createdContact = new Contact();
            contact.Id = Guid.NewGuid();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "createContact";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new SqlParameter("Id", System.Data.SqlDbType.UniqueIdentifier);
                idParam.Value = contact.Id;
                cmd.Parameters.Add(idParam);

                var firstNameParam = new SqlParameter("FirstName", System.Data.SqlDbType.NVarChar);
                firstNameParam.Value = contact.FirstName;
                cmd.Parameters.Add(firstNameParam);

                var lastNameParam = new SqlParameter("LastName", System.Data.SqlDbType.NVarChar);
                lastNameParam.Value = contact.LastName;
                cmd.Parameters.Add(lastNameParam);

                var emailParam = new SqlParameter("Email", System.Data.SqlDbType.NVarChar);
                emailParam.Value = contact.Email;
                cmd.Parameters.Add(emailParam);

                var birthdateParam = new SqlParameter("Birthdate", System.Data.SqlDbType.DateTime);
                birthdateParam.Value = contact.Birthdate;
                cmd.Parameters.Add(birthdateParam);

                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    createdContact.LoadDataFromReader(reader);
                }
            }

            return createdContact;
        }

        public Contact UpdateContact(Guid Id, Contact contact)
        {
            var updatedContact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "updateContactById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var idParam = new SqlParameter("Id", System.Data.SqlDbType.UniqueIdentifier);
                idParam.Value = Id;
                cmd.Parameters.Add(idParam);

                var firstNameParam = new SqlParameter("FirstName", System.Data.SqlDbType.NVarChar);
                firstNameParam.Value = contact.FirstName;
                cmd.Parameters.Add(firstNameParam);

                var lastNameParam = new SqlParameter("LastName", System.Data.SqlDbType.NVarChar);
                lastNameParam.Value = contact.LastName;
                cmd.Parameters.Add(lastNameParam);

                var emailParam = new SqlParameter("Email", System.Data.SqlDbType.NVarChar);
                emailParam.Value = contact.Email;
                cmd.Parameters.Add(emailParam);

                var birthdateParam = new SqlParameter("Birthdate", System.Data.SqlDbType.DateTime);
                birthdateParam.Value = contact.Birthdate;
                cmd.Parameters.Add(birthdateParam);

                var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    updatedContact.LoadDataFromReader(reader);
                }
            }

            return updatedContact;
        }

        public void RemoveContact(Guid id)
        {
            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "removeContactById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var contactId = new SqlParameter("Id", System.Data.SqlDbType.UniqueIdentifier);
                contactId.Value = id;
                cmd.Parameters.Add(contactId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
