using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationsDataAccess
    {
        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicationTypeID, ref int ApplicationPersonID, ref DateTime ApplicationDate,
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref decimal PaidFees,
            ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader r = command.ExecuteReader();

                if (r.Read())
                {
                    isFound = true;

                    ApplicationTypeID = (int)r["ApplicationTypeID"];
                    ApplicationPersonID = (int)r["ApplicationPersonID"];
                    ApplicationDate = (DateTime)r["ApplicationDate"];
                    ApplicationStatus = Convert.ToByte(r["ApplicationStatus"]);   // usually 1–4 enum
                    LastStatusDate = (DateTime)r["LastStatusDate"];
                    PaidFees = Convert.ToDecimal(r["PaidFees"]);
                    CreatedByUserID = (int)r["CreatedByUserID"];
                }

                r.Close();
            }
            catch (Exception ex)
            {
                isFound = false;
                System.Diagnostics.Debug.WriteLine($"GetApplicationInfoByID Error: {ex.Message}");
                throw new Exception($"Error retrieving application: {ex.Message}", ex);
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewApplication(int ApplicationTypeID, int ApplicationPersonID,
            DateTime ApplicationDate, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            int newID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                INSERT INTO Applications
                (ApplicationTypeID, ApplicationPersonID, ApplicationDate, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                VALUES
                (@ApplicationTypeID, @ApplicationPersonID, @ApplicationDate, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);

                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null) newID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"AddNewApplication Error: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw new Exception($"Error adding new application: {ex.Message}", ex);
            }
            finally { connection.Close(); }

            return newID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicationTypeID, int ApplicationPersonID,
            DateTime ApplicationDate, byte ApplicationStatus, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            int rows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                UPDATE Applications SET
                    ApplicationTypeID = @ApplicationTypeID,
                    ApplicationPersonID = @ApplicationPersonID,
                    ApplicationDate = @ApplicationDate,
                    ApplicationStatus = @ApplicationStatus,
                    LastStatusDate = @LastStatusDate,
                    PaidFees = @PaidFees,
                    CreatedByUserID = @CreatedByUserID
                WHERE ApplicationID = @ApplicationID
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationPersonID", ApplicationPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"UpdateApplication Error: {ex.Message}");
                throw new Exception($"Error updating application: {ex.Message}", ex);
            }
            finally { connection.Close(); }

            return rows > 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"DeleteApplication Error: {ex.Message}");
                throw new Exception($"Error deleting application: {ex.Message}", ex);
            }
            finally { connection.Close(); }

            return rows > 0;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Applications WHERE ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            object result = null;

            try
            {
                connection.Open();
                result = command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"IsApplicationExist Error: {ex.Message}");
                throw new Exception($"Error checking application existence: {ex.Message}", ex);
            }
            finally { connection.Close(); }

            return result != null;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications ORDER BY ApplicationID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader r = command.ExecuteReader();
                if (r.HasRows) dt.Load(r);
                r.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetAllApplications Error: {ex.Message}");
                throw new Exception($"Error retrieving all applications: {ex.Message}", ex);
            }
            finally { connection.Close(); }

            return dt;
        }
    }
}
