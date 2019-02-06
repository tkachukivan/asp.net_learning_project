using DBExtentionsMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using ContactsManagerDAL.Models;
using System.Linq;

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
                    contact.Address = new Address();
                    contact.Address.LoadDataFromReader(reader);
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
                contact.LastName == null
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

                if (contact.Address != null)
                {
                    contact.Address.Id = Guid.NewGuid();

                    cmd.Parameters.AddNewParameter("AddressId", SqlDbType.UniqueIdentifier, contact.Address.Id);

                    if (contact.Address.Country != null)
                    {
                        cmd.Parameters.AddNewParameter("Country", SqlDbType.NVarChar, contact.Address.Country);
                    }

                    if (contact.Address.City != null)
                    {
                        cmd.Parameters.AddNewParameter("City", SqlDbType.NVarChar, contact.Address.City);
                    }

                    if (contact.Address.Street != null)
                    {
                        cmd.Parameters.AddNewParameter("Street", SqlDbType.NVarChar, contact.Address.Street);
                    }

                    if (contact.Address.Building != null)
                    {
                        cmd.Parameters.AddNewParameter("Building", SqlDbType.NVarChar, contact.Address.Building);
                    }

                    if (contact.Address.Appartment != null)
                    {
                        cmd.Parameters.AddNewParameter("Appartment", SqlDbType.NVarChar, contact.Address.Appartment);
                    }

                    if (contact.Address.ZipCode != null)
                    {
                        cmd.Parameters.AddNewParameter("ZipCode", SqlDbType.NVarChar, contact.Address.ZipCode);
                    }
                }

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    createdContact.LoadDataFromReader(reader);

                    createdContact.Address = new Address();
                    createdContact.Address.LoadDataFromReader(reader);
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
                contact.LastName == null
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

                if (contact.Address != null)
                {
                    if (contact.Address.Id == null)
                    {
                        contact.Address.Id = Guid.NewGuid();
                    }

                    cmd.Parameters.AddNewParameter("AddressId", SqlDbType.UniqueIdentifier, contact.Address.Id);

                    if (contact.Address.Country != null)
                    {
                        cmd.Parameters.AddNewParameter("Country", SqlDbType.NVarChar, contact.Address.Country);
                    }

                    if (contact.Address.City != null)
                    {
                        cmd.Parameters.AddNewParameter("City", SqlDbType.NVarChar, contact.Address.City);
                    }

                    if (contact.Address.Street != null)
                    {
                        cmd.Parameters.AddNewParameter("Street", SqlDbType.NVarChar, contact.Address.Street);
                    }

                    if (contact.Address.Building != null)
                    {
                        cmd.Parameters.AddNewParameter("Building", SqlDbType.NVarChar, contact.Address.Building);
                    }

                    if (contact.Address.Appartment != null)
                    {
                        cmd.Parameters.AddNewParameter("Appartment", SqlDbType.NVarChar, contact.Address.Appartment);
                    }

                    if (contact.Address.ZipCode != null)
                    {
                        cmd.Parameters.AddNewParameter("ZipCode", SqlDbType.NVarChar, contact.Address.ZipCode);
                    }
                }

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    updatedContact.LoadDataFromReader(reader);

                    updatedContact.Address = new Address();
                    updatedContact.Address.LoadDataFromReader(reader);
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
