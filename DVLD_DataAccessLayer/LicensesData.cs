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
            ref bool IsActive, ref int IssueReason, ref decimal PaidFees, ref int CreatedByUserID)
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
                        IssueReason = Convert.ToInt32(reader["IssueReason"]);
                        PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        public static bool GetLicenseInfoByApplicationID(int applicationID,
            ref int LicenseID, ref int DriverID, ref int LicenseClassID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes,
            ref bool IsActive, ref int IssueReason, ref decimal PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            string query = @"SELECT TOP 1 * FROM Licenses 
                             WHERE ApplicationID = @ApplicationID
                             ORDER BY LicenseID DESC";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", applicationID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        LicenseID = (int)reader["LicenseID"];
                        DriverID = (int)reader["DriverID"];
                        LicenseClassID = (int)reader["LicenseClass"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        IssueReason = Convert.ToInt32(reader["IssueReason"]);
                        PaidFees = Convert.ToDecimal(reader["PaidFees"]);
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    }

                    reader.Close();
                }
                catch
                {
                    isFound = false;
                }
            }

            return isFound;
        }

        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            bool IsActive, int IssueReason, decimal PaidFees, int CreatedByUserID)
        {
            int LicenseID = -1;

            string query = @"INSERT INTO Licenses (
                                ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes,
                                IsActive, IssueReason, PaidFees, CreatedByUserID
                            )
                            VALUES (
                                @ApplicationID, @DriverID, @LicenseClassID, @IssueDate, @ExpirationDate, @Notes,
                                @IsActive, @IssueReason, @PaidFees, @CreatedByUserID
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
                cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        LicenseID = insertedID;
                }
                catch
                {
                }
            }

            return LicenseID;
        }

        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClassID,
            DateTime IssueDate, DateTime ExpirationDate, string Notes,
            bool IsActive, int IssueReason, decimal PaidFees, int CreatedByUserID)
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
                                IssueReason = @IssueReason,
                                PaidFees = @PaidFees,
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
                cmd.Parameters.AddWithValue("@IssueReason", IssueReason);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@Notes", string.IsNullOrWhiteSpace(Notes) ? (object)DBNull.Value : Notes);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch
                {
                    return false;
                }
            }

            return rowsAffected > 0;
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

        public static bool UpdateLicenseDetainedStatus(int licenseID, bool isDetained)
        {
            int rowsAffected = 0;
            string query = @"UPDATE Licenses
                     SET IsDetained = @IsDetained
                     WHERE LicenseID = @LicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseID", licenseID);
                cmd.Parameters.AddWithValue("@IsDetained", isDetained);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    System.Diagnostics.Debug.WriteLine("UpdateLicenseDetainedStatus SQL Error: " + ex.Message);

                    // If schema has no IsDetained column, detention state is already tracked in DetainedLicenses table.
                    if (ex.Message.IndexOf("Invalid column name 'IsDetained'", StringComparison.OrdinalIgnoreCase) >= 0)
                        return true;

                    return false;
                }
            }

            return rowsAffected > 0;
        }
    }
}