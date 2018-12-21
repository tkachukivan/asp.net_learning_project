using System.Data;
using System.Data.SqlClient;

namespace DBExtentionsMethods
{
    public static class DBExtentionsMethods
    {
        public static void AddNewParameter(this SqlParameterCollection parameter, string name, SqlDbType type, object value)
        {
            parameter.Add(name, type).Value = value;
        }
    }
}
