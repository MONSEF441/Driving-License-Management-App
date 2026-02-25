using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class clsConnection
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(clsDataAccessSettings.ConnectionString);
        }
    }
}