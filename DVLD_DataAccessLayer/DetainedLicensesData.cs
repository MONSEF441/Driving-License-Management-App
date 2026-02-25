using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsDetainedLicensesDataAccess
    {
        public static bool GetDetainedLicenseInfoByID(int DetainID,
            ref int LicenseID, ref DateTime DetainDate,
            ref decimal FineFees, ref bool IsReleased,
            ref DateTime ReleasedDate, ref int ReleasedByUserID,
            ref int ReleaseApplicationID)
        {
            bool isFound = false;

            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        LicenseID = (int)reader["LicenseID"];
                        DetainDate = (DateTime)reader["DetainDate"];
                        FineFees = (decimal)reader["FineFees"];
                        IsReleased = (bool)reader["IsReleased"];

                        // Handle nullable Release Date
                        if (reader["ReleasedDate"] == DBNull.Value)
                            ReleasedDate = DateTime.MinValue;
                        else
                            ReleasedDate = (DateTime)reader["ReleasedDate"];

                        // Handle nullable ReleasedByUserID
                        if (reader["ReleasedByUserID"] == DBNull.Value)
                            ReleasedByUserID = -1;
                        else
                            ReleasedByUserID = (int)reader["ReleasedByUserID"];

                        // Handle nullable ReleaseApplicationID
                        if (reader["ReleaseApplicationID"] == DBNull.Value)
                            ReleaseApplicationID = -1;
                        else
                            ReleaseApplicationID = (int)reader["ReleaseApplicationID"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception or log it
                    isFound = false;
                }
            }

            return isFound;
        }

        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM DetainedLicenses";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        dt.Load(reader);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return dt;
        }

        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate,
            decimal FineFees, bool IsReleased,
            DateTime ReleasedDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int DetainID = -1;

            string query = @"INSERT INTO DetainedLicenses (
                                LicenseID, 
                                DetainDate, 
                                FineFees, 
                                IsReleased, 
                                ReleasedDate, 
                                ReleasedByUserID, 
                                ReleaseApplicationID
                            )
                            VALUES (
                                @LicenseID, 
                                @DetainDate, 
                                @FineFees, 
                                @IsReleased, 
                                @ReleasedDate, 
                                @ReleasedByUserID, 
                                @ReleaseApplicationID
                            );
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@IsReleased", IsReleased);

                // Handle Nullables for Release info
                if (ReleasedDate == DateTime.MinValue)
                    command.Parameters.AddWithValue("@ReleasedDate", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleasedDate", ReleasedDate);

                if (ReleasedByUserID == -1)
                    command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

                if (ReleaseApplicationID == -1)
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        DetainID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return DetainID;
        }

        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate,
            decimal FineFees, bool IsReleased,
            DateTime ReleasedDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            int rowsAffected = 0;
            string query = @"UPDATE DetainedLicenses  
                            SET LicenseID = @LicenseID,
                                DetainDate = @DetainDate,
                                FineFees = @FineFees,
                                IsReleased = @IsReleased,
                                ReleasedDate = @ReleasedDate,
                                ReleasedByUserID = @ReleasedByUserID,
                                ReleaseApplicationID = @ReleaseApplicationID
                            WHERE DetainID = @DetainID";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);
                command.Parameters.AddWithValue("@LicenseID", LicenseID);
                command.Parameters.AddWithValue("@DetainDate", DetainDate);
                command.Parameters.AddWithValue("@FineFees", FineFees);
                command.Parameters.AddWithValue("@IsReleased", IsReleased);

                // Handle Nullables for Release info
                if (ReleasedDate == DateTime.MinValue)
                    command.Parameters.AddWithValue("@ReleasedDate", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleasedDate", ReleasedDate);

                if (ReleasedByUserID == -1)
                    command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);

                if (ReleaseApplicationID == -1)
                    command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
                else
                    command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    return false;
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteDetainedLicense(int DetainID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM DetainedLicenses WHERE DetainID = @DetainID";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return (rowsAffected > 0);
        }

        public static bool IsDetainedLicenseExist(int DetainID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM DetainedLicenses WHERE DetainID = @DetainID";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DetainID", DetainID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        public static bool IsLicenseDetained(int LicenseID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";

            using (SqlConnection connection = clsConnection.GetConnection())
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    isFound = reader.HasRows;
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }

            return isFound;
        }
    }
}