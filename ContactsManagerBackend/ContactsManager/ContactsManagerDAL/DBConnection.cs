using System.Data.SqlClient;
using System.Configuration;

namespace ContactsManagerDAL
{
    internal static class DBConnection
    {
        public static string ConnectionString
        {
            get
            {
                string connStr = ConfigurationManager.ConnectionStrings["ContactsManagerDb"].ToString();

                return connStr;
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
    }
}
