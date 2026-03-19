using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsInternationalLicensesDataAccess
    {
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable dt = new DataTable();
            string query = "select InternationalLicenseID , ApplicationID , DriverID , IssuedUsingLocalLicenseID as LocalLicenseID ,  IssueDate , ExpirationDate , IsActive from InternationalLicenses ORDER BY InternationalLicenseID DESC";

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

        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID,
            ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID,
            ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        ApplicationID = (int)reader["ApplicationID"];
                        DriverID = (int)reader["DriverID"];
                        IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                        IssueDate = (DateTime)reader["IssueDate"];
                        ExpirationDate = (DateTime)reader["ExpirationDate"];
                        IsActive = (bool)reader["IsActive"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
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

        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int InternationalLicenseID = -1;

            string query = @"INSERT INTO InternationalLicenses
                            (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID)
                            VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                cmd.Parameters.AddWithValue("@DriverID", DriverID);
                cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        InternationalLicenseID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return InternationalLicenseID;
        }

        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE InternationalLicenses SET
                            ApplicationID = @ApplicationID,
                            DriverID = @DriverID,
                            IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID,
                            IssueDate = @IssueDate,
                            ExpirationDate = @ExpirationDate,
                            IsActive = @IsActive,
                            CreatedByUserID = @CreatedByUserID
                            WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
                cmd.Parameters.AddWithValue("@ApplicationID", ApplicationID);
                cmd.Parameters.AddWithValue("@DriverID", DriverID);
                cmd.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
                cmd.Parameters.AddWithValue("@IssueDate", IssueDate);
                cmd.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
                cmd.Parameters.AddWithValue("@IsActive", IsActive);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

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

        public static bool DeleteInternationalLicense(int InternationalLicenseID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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

        public static bool IsInternationalLicenseExist(int InternationalLicenseID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

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