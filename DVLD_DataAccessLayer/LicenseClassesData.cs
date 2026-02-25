using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsLicenseClassesDataAccess
    {
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM LicenseClasses";

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

        public static bool GetLicenseClassInfoByID(int LicenseClassID,
            ref string ClassName, ref string ClassDescription,
            ref int MinimumAllowedAge, ref int DefaultValidityLength, ref decimal ClassFees)
        {
            bool isFound = false;

            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        ClassName = (string)reader["ClassName"];
                        ClassDescription = (string)reader["ClassDescription"];
                        MinimumAllowedAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                        DefaultValidityLength = Convert.ToInt32(reader["DefaultValidityLength"]);
                        ClassFees = Convert.ToDecimal(reader["ClassFees"]);
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

        public static int AddNewLicenseClass(string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int LicenseClassID = -1;

            string query = @"INSERT INTO LicenseClasses
                            (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees)
                            VALUES (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees);
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClassName", ClassName);
                cmd.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                cmd.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
                cmd.Parameters.AddWithValue("@ClassFees", ClassFees);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        LicenseClassID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return LicenseClassID;
        }

        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription,
            int MinimumAllowedAge, int DefaultValidityLength, decimal ClassFees)
        {
            int rowsAffected = 0;

            string query = @"UPDATE LicenseClasses SET
                            ClassName = @ClassName,
                            ClassDescription = @ClassDescription,
                            MinimumAllowedAge = @MinimumAllowedAge,
                            DefaultValidityLength = @DefaultValidityLength,
                            ClassFees = @ClassFees
                            WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
                cmd.Parameters.AddWithValue("@ClassName", ClassName);
                cmd.Parameters.AddWithValue("@ClassDescription", ClassDescription);
                cmd.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
                cmd.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
                cmd.Parameters.AddWithValue("@ClassFees", ClassFees);

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

        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool IsLicenseClassExist(int LicenseClassID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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

        public static bool IsLicenseClassExist(string ClassName)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM LicenseClasses WHERE ClassName = @ClassName";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@ClassName", ClassName);

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