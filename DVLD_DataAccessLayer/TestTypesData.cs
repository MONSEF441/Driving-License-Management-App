using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DVLD_DataAccess
{
    public class clsTestTypesDataAccess
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM TestTypes";

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
                    MessageBox.Show(ex.Message);
                }
            }

            return dt;
        }

        public static bool GetTestTypeInfoByID(int TestTypeID,
            ref string TestTypeTitle, ref string TestTypeDescription, ref decimal TestTypeFees)
        {
            bool isFound = false;

            string query = @"SELECT TestTypeTitle, TestTypeDescription, TestTypeFees
                             FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;
                        TestTypeTitle = (string)reader["TestTypeTitle"];
                        TestTypeDescription = (string)reader["TestTypeDescription"];
                        TestTypeFees = Convert.ToDecimal(reader["TestTypeFees"]);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    isFound = false;
                }
            }

            return isFound;
        }

        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int TestTypeID = -1;

            string query = @"INSERT INTO TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees)
                             VALUES (@TestTypeTitle, @TestTypeDescription, @TestTypeFees);
                             SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                cmd.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                cmd.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TestTypeID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return TestTypeID;
        }

        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int rowsAffected = 0;

            string query = @"UPDATE TestTypes SET 
                             TestTypeTitle = @TestTypeTitle,
                             TestTypeDescription = @TestTypeDescription,
                             TestTypeFees = @TestTypeFees
                             WHERE TestTypeID = @TestTypeID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
                cmd.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
                cmd.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

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

        public static bool DeleteTestType(int TestTypeID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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

        public static bool IsTestTypeExist(int TestTypeID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM TestTypes WHERE TestTypeID = @TestTypeID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

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