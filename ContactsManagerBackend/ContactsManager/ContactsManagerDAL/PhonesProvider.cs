using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ContactsManagerDAL.Models;
using DBExtentionsMethods;

namespace ContactsManagerDAL
{
    public class PhonesProvider
    {
        public List<Phone> GetPhones(Guid contactId)
        {
            var Phones = new List<Phone>();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContactPhones";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddNewParameter("ContactId", SqlDbType.UniqueIdentifier, contactId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var phone = new Phone();

                        phone.LoadDataFromReader(reader);

                        Phones.Add(phone);
                    }
                }
            }

            return Phones;
        }

        public Phone GetPhone(Guid contactId, Guid Id)
        {
            Phone phone = null;

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "getContactPhoneById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, Id);
                cmd.Parameters.AddNewParameter("ContactId", SqlDbType.UniqueIdentifier, contactId);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    phone = new Phone();
                    phone.LoadDataFromReader(reader);
                }
            }

            return phone;
        }

        public Phone CreatePhone(Guid contactId, Phone phone)
        {
            if (phone?.LocalNumber.Number == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }

            var createdPhone = new Phone();
            phone.Id = Guid.NewGuid();

            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "createContactPhone";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, phone.Id);
                cmd.Parameters.AddNewParameter("ContactId", SqlDbType.UniqueIdentifier, contactId);
                cmd.Parameters.AddNewParameter("CountryCode", SqlDbType.NVarChar, phone.LocalNumber.CountryCode);
                cmd.Parameters.AddNewParameter("PhoneNumber", SqlDbType.NVarChar, phone.LocalNumber.Number);
                cmd.Parameters.AddNewParameter("PhoneType", SqlDbType.Int, phone.PhoneType);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                if (reader.Read())
                {
                    createdPhone.LoadDataFromReader(reader);
                }
            }

            return createdPhone;
        }

        public void UpdatePhone(Guid contactId, Guid Id, Phone phone)
        {
            if (phone?.LocalNumber.Number == null)
            {
                throw new ArgumentNullException(nameof(phone));
            }


            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "updateContactPhoneById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, phone.Id);
                cmd.Parameters.AddNewParameter("ContactId", SqlDbType.UniqueIdentifier, contactId);
                cmd.Parameters.AddNewParameter("CountryCode", SqlDbType.NVarChar, phone.LocalNumber.CountryCode);
                cmd.Parameters.AddNewParameter("PhoneNumber", SqlDbType.NVarChar, phone.LocalNumber.Number);
                cmd.Parameters.AddNewParameter("PhoneType", SqlDbType.Int, phone.PhoneType);

                cmd.ExecuteNonQuery();
            }
        }

        public void RemovePhone(Guid contactId, Guid Id)
        {
            using (SqlConnection conn = DBConnection.GetSqlConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "removeContactPhoneById";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddNewParameter("Id", SqlDbType.UniqueIdentifier, Id);
                cmd.Parameters.AddNewParameter("ContactId", SqlDbType.UniqueIdentifier, contactId);

                cmd.ExecuteNonQuery();
            }
        }
    }
}
