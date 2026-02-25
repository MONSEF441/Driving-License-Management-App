using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_DataAccess
{
    public class clsApplicationTypesDataAccess
    {
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID,
            ref string ApplicationTypeTitle , ref decimal ApplicationTypeFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader r = command.ExecuteReader();

                if (r.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)r["ApplicationTypeTitle"];
                    ApplicationTypeFees =Convert.ToDecimal(r["ApplicationTypeFees"]);
                }

                r.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                isFound = false;
            }
            finally { connection.Close(); }

            return isFound;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID , string ApplicationTypeTitle , decimal ApplicationTypeFees)
        {
            int rows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                UPDATE ApplicationTypes SET
                  ApplicationTypeTitle = @ApplicationTypeTitle ,
                  ApplicationTypeFees =  @ApplicationTypeFees
                WHERE ApplicationTypeID = @ApplicationTypeID;
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationTypeFees", ApplicationTypeFees);

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return rows > 0;
        }
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes ORDER BY ApplicationTypeID";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                if (r.HasRows) dt.Load(r);
                r.Close();
            }
            catch
            {
            }
            finally { connection.Close(); }

            return dt;
        }
    }
}
