using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class clsPersonDataAccess
    {
        public static bool GetPersonInfoByID(int PersonID,
      ref string CIN, ref string FirstName, ref string LastName, ref DateTime DateOfBirth, ref bool Gender,
      ref string Address, ref string Phone, ref string Email,
      ref int NationalityCountryID, ref string ImagePath)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SELECT * FROM People WHERE PersonID = @PersonID", connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;

                            CIN = reader["CIN"].ToString();
                            FirstName = reader["FirstName"].ToString();
                            LastName = reader["LastName"].ToString();
                            DateOfBirth = (DateTime)reader["DateOfBirth"];
                            Gender = ((string)reader["Gender"]).Trim().ToLower() == "male";
                            Address = reader["Address"].ToString();
                            Phone = reader["Phone"].ToString();
                            Email = reader["Email"] == DBNull.Value ? "" : reader["Email"].ToString();
                            NationalityCountryID = (int)reader["NationalityCountryID"];
                            ImagePath = reader["ImagePath"] == DBNull.Value ? "" : reader["ImagePath"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    isFound = false;
                    // Optionally log ex.Message
                }
            }

            return isFound;
        }


        public static int AddNewPerson(
            string CIN, string FirstName, string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int newID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                INSERT INTO People 
                (CIN, FirstName, LastName, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath)
                VALUES
                (@CIN, @FirstName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);

                SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@CIN", CIN);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender ? "Male" : "Female");
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null)
                    newID = Convert.ToInt32(result);
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

        public static bool UpdatePerson(
            int PersonID, string CIN, string FirstName,string LastName, DateTime DateOfBirth, bool Gender, string Address,
            string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"
                UPDATE People SET
                    CIN = @CIN,
                    FirstName = @FirstName,
                    LastName = @LastName,
                    DateOfBirth = @DateOfBirth,
                    Gender = @Gender,
                    Address = @Address,
                    Phone = @Phone,
                    Email = @Email,
                    NationalityCountryID = @NationalityCountryID,
                    ImagePath = @ImagePath
                WHERE PersonID = @PersonID
            ";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CIN", CIN);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender ? "Male" : "Female");
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            command.Parameters.AddWithValue("@ImagePath", (object)ImagePath ?? DBNull.Value);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static DataTable GetAllPeople()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select PersonID , CIN , FirstName + ' ' +LastName as FullName , DateOfBirth , Gender , Phone , Email , countries.CountryName as Nationality , Address  , ImagePath  " +
                "from People join Countries on People.NationalityCountryID = Countries.CountryID ;";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) dt.Load(reader);

                reader.Close();
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

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "DELETE FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch
            {
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsPersonExist(int PersonID)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            object result = null;

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

            return (result != null);
        }

        public static bool IsCINExist(string CIN,int excludePersonID = -1)
        {
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT 1 FROM People WHERE CIN = @CIN AND PersonID <> @excludePersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CIN", CIN);
            command.Parameters.AddWithValue("@excludePersonID", excludePersonID);

            object result = null;

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

            return (result != null);
        }
        public static bool IsUser(int personID)
        {
            using (SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString))
            {
                string query = "SELECT 1 FROM Users WHERE PersonID = @PersonID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@PersonID", personID);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }

        }
    }
}
   
