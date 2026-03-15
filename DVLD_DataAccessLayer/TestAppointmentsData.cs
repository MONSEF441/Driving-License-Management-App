using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentsDataAccess
    {
        public static DataTable GetAllTestAppointments()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM TestAppointments ORDER BY AppointmentDate DESC";

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

        public static bool GetTestAppointmentInfoByID(int AppointmentID,
            ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
            ref DateTime AppointmentDate, ref decimal PaidFees,
            ref int CreatedByUserID, ref bool IsLocked)
        {
            bool isFound = false;

            string query = "SELECT * FROM TestAppointments WHERE AppointmentID = @AppointmentID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        isFound = true;

                        TestTypeID = (int)reader["TestTypeID"];
                        LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                        AppointmentDate = (DateTime)reader["AppointmentDate"];
                        PaidFees = (decimal)reader["PaidFees"];
                        CreatedByUserID = (int)reader["CreatedByUserID"];
                        IsLocked = (bool)reader["IsLocked"];
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

        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int AppointmentID = -1;

            string query = @"INSERT INTO TestAppointments (
                                TestTypeID, 
                                LocalDrivingLicenseApplicationID, 
                                AppointmentDate, 
                                PaidFees, 
                                CreatedByUserID, 
                                IsLocked
                            )
                            VALUES (
                                @TestTypeID, 
                                @LocalDrivingLicenseApplicationID, 
                                @AppointmentDate, 
                                @PaidFees, 
                                @CreatedByUserID, 
                                @IsLocked
                            );
                            SELECT SCOPE_IDENTITY();";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@IsLocked", IsLocked);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        AppointmentID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return AppointmentID;
        }

        public static bool UpdateTestAppointment(int AppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
            DateTime AppointmentDate, decimal PaidFees, int CreatedByUserID, bool IsLocked)
        {
            int rowsAffected = 0;

            string query = @"UPDATE TestAppointments SET 
                                TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked = @IsLocked
                            WHERE AppointmentID = @AppointmentID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);
                cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
                cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
                cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
                cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
                cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
                cmd.Parameters.AddWithValue("@IsLocked", IsLocked);

                try
                {
                    conn.Open();
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return false;
                }
            }

            return (rowsAffected > 0);
        }

        public static bool DeleteTestAppointment(int AppointmentID)
        {
            int rowsAffected = 0;
            string query = "DELETE FROM TestAppointments WHERE AppointmentID = @AppointmentID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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

        public static bool IsTestAppointmentExist(int AppointmentID)
        {
            bool isFound = false;
            string query = "SELECT Found=1 FROM TestAppointments WHERE AppointmentID = @AppointmentID";

            using (SqlConnection conn = clsConnection.GetConnection())
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AppointmentID", AppointmentID);

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

        public static DataTable GetTestAppointmentsByLocalDLAppAndTestType(int localDLApplicationID, int testTypeID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = @"SELECT AppointmentID, AppointmentDate, PaidFees, IsLocked 
                        FROM TestAppointments 
                        WHERE LocalDrivingLicenseApplicationID = @LocalDLApplicationID 
                        AND TestTypeID = @TestTypeID
                        ORDER BY AppointmentDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LocalDLApplicationID", localDLApplicationID);
                    command.Parameters.AddWithValue("@TestTypeID", testTypeID);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                dt.Load(reader);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log error
                    }
                }
            }

            return dt;
        }
    }
}