using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDriversDataAccess
    {
        public static bool GetDriverInfoByID(int DriverID,
            ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    CreatedDate = (DateTime)reader["CreatedDate"];
                }

                reader.Close();
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

        public static int AddNewDriver(int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int newID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate)
                VALUES (@PersonID, @CreatedByUserID, @CreatedDate);
                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null) newID = Convert.ToInt32(result);
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return newID;
        }

        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            int rows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                UPDATE Drivers SET
                    PersonID = @PersonID,
                    CreatedByUserID = @CreatedByUserID,
                    CreatedDate = @CreatedDate
                WHERE DriverID = @DriverID;
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", CreatedDate);

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

        public static bool DeleteDriver(int DriverID)
        {
            int rows = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

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

        public static bool IsDriverExist(int DriverID)
        {
            object result = null;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT 1 FROM Drivers WHERE DriverID = @DriverID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);

            try
            {
                connection.Open();
                result = command.ExecuteScalar();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return result != null;
        }

        public static DataTable GetAllDrivers()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers ORDER BY DriverID DESC";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                using (SqlDataReader r = command.ExecuteReader())
                {
                    if (r.HasRows) dt.Load(r);
                }
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
    }
}
