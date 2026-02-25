using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsApplicationsDataAccess
    {
        public static bool GetApplicationInfoByID(int ApplicationID,
            ref int ApplicationTypeID, ref int ApplicantPersonID, ref DateTime ApplicationDate,
            ref byte Status, ref DateTime LastStatusDate, ref decimal PaidFees,
            ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications WHERE Application = @Application";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Application", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader r = command.ExecuteReader();

                if (r.Read())
                {
                    isFound = true;

                    ApplicationTypeID = (int)r["ApplicationTypeID"];
                    ApplicantPersonID = (int)r["ApplicantPersonID"];
                    ApplicationDate = (DateTime)r["ApplicationDate"];
                    Status = Convert.ToByte(r["Status"]);   // usually 1–4 enum
                    LastStatusDate = (DateTime)r["LastStatusDate"];
                    PaidFees = Convert.ToDecimal(r["PaidFees"]);
                    CreatedByUserID = (int)r["CreatedByUserID"];
                }

                r.Close();
            }
            catch
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }

        public static int AddNewApplication(int ApplicationTypeID, int ApplicantPersonID,
            DateTime ApplicationDate, byte Status, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            int newID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                INSERT INTO Applications
                (ApplicationTypeID, ApplicantPersonID, ApplicationDate, Status, LastStatusDate, PaidFees, CreatedByUserID)
                VALUES
                (@ApplicationTypeID, @ApplicantPersonID, @ApplicationDate, @Status, @LastStatusDate, @PaidFees, @CreatedByUserID);

                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null) newID = Convert.ToInt32(result);
            }
            catch
            {
            }
            finally { connection.Close(); }

            return newID;
        }

        public static bool UpdateApplication(int ApplicationID, int ApplicationTypeID, int ApplicantPersonID,
            DateTime ApplicationDate, byte Status, DateTime LastStatusDate,
            decimal PaidFees, int CreatedByUserID)
        {
            int rows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                UPDATE Applications SET
                    ApplicationTypeID = @ApplicationTypeID,
                    ApplicantPersonID = @ApplicantPersonID,
                    ApplicationDate = @ApplicationDate,
                    Status = @Status,
                    LastStatusDate = @LastStatusDate,
                    PaidFees = @PaidFees,
                    CreatedByUserID = @CreatedByUserID
                WHERE Application = @Application
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Application", ApplicationID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally { connection.Close(); }

            return rows > 0;
        }

        public static bool DeleteApplication(int ApplicationID)
        {
            int rows = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE Application = @Application";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Application", ApplicationID);

            try
            {
                connection.Open();
                rows = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally { connection.Close(); }

            return rows > 0;
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM Applications WHERE Application = @Application";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Application", ApplicationID);

            object result = null;

            try
            {
                connection.Open();
                result = command.ExecuteScalar();
            }
            catch
            {
            }
            finally { connection.Close(); }

            return result != null;
        }

        public static DataTable GetAllApplications()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Applications ORDER BY Application DESC";

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
