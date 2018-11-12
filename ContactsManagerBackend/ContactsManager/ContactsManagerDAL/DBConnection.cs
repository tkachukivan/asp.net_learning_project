using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace ContactsManagerDAL
{
    class DBConnection
    {
        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["ContactsManagerDb"].ToString();

                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(connStr);

                return sb.ToString();
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            // Open the database connection with retries
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
