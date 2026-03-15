using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestsDataAccess
    {
        public static DataTable GetAllTests()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Tests";

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

        public static bool GetTestInfoByID(int TestID,
            ref int TestAppointmentID, ref bool TestResult,
            ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;

            string query = "SELECT * FROM Tests WHERE TestID = @TestID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", TestID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        TestAppointmentID = (int)reader["TestAppointmentID"];
                        TestResult = (bool)reader["TestResult"];
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

        public static bool IsTestPassed(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isPassed = false;

            string query = @"SELECT TOP 1 T.TestResult 
                           FROM Tests T
                           INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                           WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                           AND TA.TestTypeID = @TestTypeID
                           ORDER BY T.TestID DESC";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        isPassed = Convert.ToBoolean(result);
                    }
                }
                catch (Exception ex)
                {
                    isPassed = false;
                }
            }

            return isPassed;
        }

        public static int GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            int count = 0;

            string query = @"SELECT COUNT(*) 
                           FROM Tests T
                           INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                           WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                           AND T.TestResult = 1";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        count = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    count = 0;
                }
            }

            return count;
        }

        public static byte GetTestTrialCount(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte count = 0;

            string query = @"SELECT COUNT(*) 
                           FROM Tests T
                           INNER JOIN TestAppointments TA ON T.TestAppointmentID = TA.TestAppointmentID
                           WHERE TA.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID 
                           AND TA.TestTypeID = @TestTypeID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        count = Convert.ToByte(result);
                    }
                }
                catch (Exception ex)
                {
                    count = 0;
                }
            }

            return count;
        }

        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = -1;

            string query = @"INSERT INTO Tests (
                                TestAppointmentID, 
                                TestResult, 
                                Notes, 
                                CreatedByUserID
                            )
                            VALUES (
                                @TestAppointmentID, 
                                @TestResult, 
                                @Notes, 
                                @CreatedByUserID
                            );
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                cmd.Parameters.AddWithValue("@TestResult", TestResult);
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
                        TestID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return TestID;
        }

        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult,
            string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;

            string query = @"UPDATE Tests SET 
                                TestAppointmentID = @TestAppointmentID,
                                TestResult = @TestResult,
                                Notes = @Notes,
                                CreatedByUserID = @CreatedByUserID
                            WHERE TestID = @TestID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", TestID);
                cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
                cmd.Parameters.AddWithValue("@TestResult", TestResult);
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

        public static bool DeleteTest(int TestID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM Tests WHERE TestID = @TestID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", TestID);

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

        public static bool IsTestExist(int TestID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM Tests WHERE TestID = @TestID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestID", TestID);

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

        public static int GetTestIDByTestAppointmentID(int TestAppointmentID)
        {
            int testID = -1;

            string query = "SELECT TestID FROM Tests WHERE TestAppointmentID = @TestAppointmentID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        testID = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    // Handle ex
                }
            }

            return testID;
        }
    }
}