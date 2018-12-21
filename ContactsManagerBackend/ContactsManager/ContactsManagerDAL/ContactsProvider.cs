using DBExtentionsMethods;
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
            var contact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, Id);

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
                contact.Email == null
                )
            {
                throw new ArgumentNullException();
            }

            var createdContact = new Contact();
            contact.Id = Guid.NewGuid();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "createContact";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, contact.Id);

                cmd.Parameters.AddNewParameter("FirstName", SqlDbType.NVarChar, contact.FirstName);

                cmd.Parameters.AddNewParameter("LastName", SqlDbType.NVarChar, contact.LastName);

                cmd.Parameters.AddNewParameter("Email", SqlDbType.NVarChar, contact.Email);

                if (contact.Birthdate != null)
                {
                    cmd.Parameters.AddNewParameter("Birthdate", SqlDbType.DateTime, contact.Birthdate);
                }

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
                throw new ArgumentNullException();
            }

            var updatedContact = new Contact();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "updateContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, Id);

                cmd.Parameters.AddNewParameter("FirstName", SqlDbType.NVarChar, contact.FirstName);

                cmd.Parameters.AddNewParameter("LastName", SqlDbType.NVarChar, contact.LastName);
                
                cmd.Parameters.AddNewParameter("Email", SqlDbType.NVarChar, contact.Email);

                if (contact.Birthdate != null)
                {
                    cmd.Parameters.AddNewParameter("Birthdate", SqlDbType.DateTime, contact.Birthdate);
                }

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
            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "removeContactById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, Id);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
