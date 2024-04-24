using System;
using System.Data;
using System.Data.SqlClient;

namespace DataAccessLayer
{
   public class clsContactsData
   {
      public static bool DeleteContactByCountryID(int CountryID)
      {
         int NbrRowsAffected = 0;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);
         string query = @"Delete from Contacts where CountryID = @CountryID;";
         SqlCommand command = new SqlCommand(query, connection);
         command.Parameters.AddWithValue("@CountryID", CountryID);

         try
         {
            connection.Open();
            NbrRowsAffected = command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            Console.WriteLine(" error " + ex.Message);
         }
         finally
         {
            connection.Close();
         }
         return (NbrRowsAffected > 0);
      }
      public static bool isContactExist(int Id)
      {
         bool IsFound = false;

         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "SELECT found = 1 FROM Contacts WHERE ContactID = @ContactID";

         SqlCommand command = new SqlCommand(query, connection);

         command.Parameters.AddWithValue("@ContactID", Id);

         try
         {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            IsFound = reader.HasRows;
         }
         catch (Exception ex)
         {
            Console.WriteLine("error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }
         return IsFound;
      }
      public static DataTable GetAllContactsInfo()
      {
         DataTable dt = new DataTable();
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "SELECT * FROM Contacts ";

         SqlCommand command = new SqlCommand(query, connection);

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
            Console.WriteLine("Error: " + ex.Message);
         }
         finally
         {
            connection.Close();
         }

         return dt;
      }
      public static bool DeleteContact(int ContactID)
      {
         int NbrRowsAffected = 0;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);
         string query = @"Delete from Contacts where ContactID = @contactID;";
         SqlCommand command = new SqlCommand(query, connection);
         command.Parameters.AddWithValue("@ContactID", ContactID);

         try
         {
            connection.Open();
            NbrRowsAffected = command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            Console.WriteLine(" error " + ex.Message);
         }
         finally
         {
            connection.Close();
         }
         return (NbrRowsAffected > 0);
      }
      public static bool UpdateContact(int ContactID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
      {
         int NbrRowsAffected = 0;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);
         string query = @"update Contacts  
                 set FirstName = @FirstName,LastName =  @LastName, Email = @Email,
                     Phone= @Phone, Address= @Address, DateOfBirth = @DateOfBirth, CountryID= @CountryID , ImagePath = @ImagePath
                     where ContactID = @contactID;";
         SqlCommand command = new SqlCommand(query, connection);
         command.Parameters.AddWithValue("@FirstName", FirstName);
         command.Parameters.AddWithValue("@LastName", LastName);
         command.Parameters.AddWithValue("@Email", Email);
         command.Parameters.AddWithValue("@Phone", Phone);
         command.Parameters.AddWithValue("@Address", Address);
         command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
         command.Parameters.AddWithValue("@CountryID", CountryID);
         command.Parameters.AddWithValue("@ContactID", ContactID);
         if (ImagePath != "")
         {
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
         }
         else
         {
            command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
         }
         try
         {
            connection.Open();
            NbrRowsAffected = command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            Console.WriteLine(" error " + ex.Message);
         }
         finally
         {
            connection.Close();
         }
         return (NbrRowsAffected > 0);

      }
      public static int AddContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
      {
         int ContactID = -1;

         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = @"INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address,DateOfBirth, CountryID,ImagePath) 
                  VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@DateOfBirth, @CountryID,@ImagePath);
                  select SCOPE_IDENTITY();";

         SqlCommand command = new SqlCommand(query, connection);
         command.Parameters.AddWithValue("@FirstName", FirstName);
         command.Parameters.AddWithValue("@LastName", LastName);
         command.Parameters.AddWithValue("@Email", Email);
         command.Parameters.AddWithValue("@Phone", Phone);
         command.Parameters.AddWithValue("@Address", Address);
         command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
         command.Parameters.AddWithValue("@CountryID", CountryID);
         if (ImagePath != null)
         {
            command.Parameters.AddWithValue("@ImagePath", ImagePath);
         }
         else
         {
            command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
         }

         try
         {
            connection.Open();
            object result = command.ExecuteScalar();
            if (result != null && int.TryParse(result.ToString(), out int InsertedID))
            {
               ContactID = InsertedID;
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }
         return ContactID;
      }

      public static bool GetContactInfoByID(int ID, ref string FirstName, ref string LastName,
            ref string Email, ref string Phone, ref string Address,
            ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
      {
         bool isFound = false;

         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

         SqlCommand command = new SqlCommand(query, connection);

         command.Parameters.AddWithValue("@ContactID", ID);

         try
         {
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {

               // The record was found
               isFound = true;

               FirstName = (string)reader["FirstName"];
               LastName = (string)reader["LastName"];
               Email = (string)reader["Email"];
               Phone = (string)reader["Phone"];
               Address = (string)reader["Address"];
               DateOfBirth = (DateTime)reader["DateOfBirth"];
               CountryID = (int)reader["CountryID"];
               if (reader["ImagePath"] == DBNull.Value)
               {
                  ImagePath = (string)reader["ImagePath"];
               }
               else
               {
                  ImagePath = "";
               }

            }
            else
            {
               // The record was not found
               isFound = false;
            }

            reader.Close();


         }
         catch (Exception ex)
         {
            Console.WriteLine("Error: " + ex.Message);
            isFound = false;
         }
         finally
         {
            connection.Close();
         }

         return isFound;
      }

   }
}

