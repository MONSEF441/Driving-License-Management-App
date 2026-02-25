using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicensesDataAccess
    {
        public static DataTable GetAllLicenses()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Licenses";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                try
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dt.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return dt;
        }

        public static bool GetLicenseInfoByID(int LicenseID,
            ref int ApplicationID, ref int DriverID, ref int LicenseClassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        ApplicationID = (int)reader["ApplicationID"];
                        DriverID = (int)reader["DriverID"];
                        LicenseClassID = (int)reader["LicenseClass"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];

                        if (reader["Notes"] == DBNull.Value)
                            Notes = "";
                        else
                            Notes = (string)reader["Notes"];
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            bool IsActive, int CreatedByUserID)
        {
            int LicenseID = -1;

            // Note: Removed PaidFees and IssueReason from INSERT as they are not in the Business Layer.
            // Ensure your Database has default values for these columns if they are NOT NULL.
            string query = @"INSERT INTO Licenses (
                                ApplicationID, 
                                DriverID, 
                                LicenseClass, 
                                IssueDate, 
                                ExpirationDate, 
                                Notes,
                                IsActive, 
                                CreatedByUserID
                            )
                            VALUES (
                                @ApplicationID, 
                                @DriverID, 
                                @LicenseClassID, 
                                @IssueDate, 
                                @ExpirationDate, 
                                @Notes, 
                                @IsActive, 
                                @CreatedByUserID
                            );
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                cmd.Parameters.AddWithValue("@DriverID", DriverID);
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                if (string.IsNullOrEmpty(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", Notes);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        LicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Licenses SET
                                ApplicationID = @ApplicationID,
                                DriverID = @DriverID,
                                LicenseClass = @LicenseClassID,
                                IssueDate = @IssueDate,
                                ExpirationDate = @ExpirationDate,
                                Notes = @Notes,
                                IsActive = @IsActive,
                                CreatedByUserID = @CreatedByUserID
                            WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                cmd.Parameters.AddWithValue("@DriverID", DriverID);
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                if (string.IsNullOrEmpty(Notes))
                    cmd.Parameters.AddWithValue("@Notes", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Notes", Notes);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                    return false;
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteLicense(int LicenseID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return (rowsAffected > 0);
        }

        public static bool IsLicenseExist(int LicenseID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Licenses WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", LicenseID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
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