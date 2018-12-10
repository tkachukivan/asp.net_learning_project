using System;
using System.Collections.Generic;
using System.Data;
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
                cmd.CommandType = CommandType.StoredProcedure;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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

        public Contact GetContact(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentException();
            }

            var contact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier);
                cmd.Parameters["Id"].Value = Id;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

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
            if (
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null ||
                contact.Birthdate == null
                )
            {
                throw new ArgumentException();
            }

            var createdContact = new Contact();
            contact.Id = Guid.NewGuid();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "createContact";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier);
                cmd.Parameters["Id"].Value = contact.Id;

                cmd.Parameters.Add("FirstName", SqlDbType.NVarChar);
                cmd.Parameters["FirstName"].Value = contact.FirstName;

                cmd.Parameters.Add("LastName", SqlDbType.NVarChar);
                cmd.Parameters["LastName"].Value = contact.LastName;

                cmd.Parameters.Add("Email", SqlDbType.NVarChar);
                cmd.Parameters["Email"].Value = contact.Email;

                cmd.Parameters.Add("Birthdate", SqlDbType.DateTime);
                cmd.Parameters["Birthdate"].Value = contact.Birthdate;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    createdContact.LoadDataFromReader(reader);
                }
            }

            return createdContact;
        }

        public Contact UpdateContact(Guid Id, Contact contact)
        {
            if (
                Id == null ||
                contact == null ||
                contact.FirstName == null ||
                contact.LastName == null ||
                contact.Email == null
                )
            {
                throw new ArgumentException();
            }

            var updatedContact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "updateContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier);
                cmd.Parameters["Id"].Value = Id;

                cmd.Parameters.Add("FirstName", SqlDbType.NVarChar);
                cmd.Parameters["FirstName"].Value = contact.FirstName;

                cmd.Parameters.Add("LastName", SqlDbType.NVarChar);
                cmd.Parameters["LastName"].Value = contact.LastName;

                cmd.Parameters.Add("Email", SqlDbType.NVarChar);
                cmd.Parameters["Email"].Value = contact.Email;

                cmd.Parameters.Add("Birthdate", SqlDbType.DateTime);
                cmd.Parameters["Birthdate"].Value = contact.Birthdate;

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    updatedContact.LoadDataFromReader(reader);
                }
            }

            return updatedContact;
        }

        public void RemoveContact(Guid Id)
        {
            if (Id == null)
            {
                throw new ArgumentException();
            }

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "removeContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("Id", SqlDbType.UniqueIdentifier);
                cmd.Parameters["Id"].Value = Id;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
