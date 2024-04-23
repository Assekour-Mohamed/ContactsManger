using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;

namespace DataAccessLayer
{
   public class clsCountriesData
   {
      public static bool IsCountryExist(string CountryName)
      {
         bool IsFound = false;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "select found = 1 from Countries where CountryName = @CountryName";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryName", CountryName);
         try
         {
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
               IsFound = true;
            }

            Reader.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
            IsFound = false;
         }
         finally
         {
            connection.Close();
         }

         return IsFound;
      }

      public static bool IsCountryExist(int CountryID)
      {
         bool IsFound = false;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "select found = 1 from Countries where CountryID = @CountryID";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryID", CountryID);
         try
         {
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
               IsFound = true;
            }

            Reader.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
            IsFound = false;
         }
         finally
         {
            connection.Close();
         }

         return IsFound;
      }

      public static DataTable GetAllCountriesInfo()
      {
         DataTable dt = new DataTable();

         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "select * from Countries order by CountryName";
         SqlCommand Command = new SqlCommand(query, connection);
         try
         {
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.HasRows)
            {
               dt.Load(Reader);
            }
            
            Reader.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }


         return dt;
      }
     
      public static bool DeleteCountry(int CountryID)
      {
         int NbrRowsAffected = 0;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "delete from Countries where CountryID = @CountryID";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryID", CountryID);
         try
         {
            connection.Open();
            NbrRowsAffected = Command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }

         return (NbrRowsAffected >0);
      }
      public static bool UpdateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
      {
         int NbrRowsAffected = 0;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "Update Countries set CountryName = @CountryName, Code = @Code , PhoneCode = @PhoneCode" +
            " where CountryID = @CountryID";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryID", CountryID);
         Command.Parameters.AddWithValue("@CountryName", CountryName);
         if (Code != "")
         {
            Command.Parameters.AddWithValue("@Code", Code);
         }
         else 
         {
            Command.Parameters.AddWithValue("@Code", DBNull.Value);

         }
         if (PhoneCode != "")
         {
            Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
         }
         else
         {
            Command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);

         }

         try
         {
            connection.Open();
            NbrRowsAffected = Command.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }

         return (NbrRowsAffected >0);
      }
      public static int AddCountry(string CountryName, string Code, string PhoneCode)
      {
         int CountryID = -1;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = @"Insert into Countries  values( @CountryName , @Code , @PhoneCode ); select SCOPE_IDENTITY() ";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryName", CountryName);
         if (Code != "")
         {
            Command.Parameters.AddWithValue("@Code", Code);
         }
         else
         {
            Command.Parameters.AddWithValue("@Code", DBNull.Value);

         }
         if (PhoneCode != "")
         {
            Command.Parameters.AddWithValue("@PhoneCode", PhoneCode);
         }
         else
         {
            Command.Parameters.AddWithValue("@PhoneCode", DBNull.Value);

         }
         try
         {
            connection.Open();
            object res = Command.ExecuteScalar();
            if (res != DBNull.Value && int.TryParse(res.ToString(), out int InsertedID))
            {
               CountryID = InsertedID;
            }
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
         }
         finally
         {
            connection.Close();
         }

         return CountryID;
      }
      public static bool GetCountryInfoByID(int CountryID, ref string CountryName, ref string Code , ref string PhoneCode)
      {
         bool IsFound = false;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "select * from Countries where CountryID = @CountryID";
         SqlCommand Command = new SqlCommand(query, connection);
         Command.Parameters.AddWithValue("@CountryID", CountryID);
         try
         {
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if(Reader.Read())
            {
               IsFound = true;
               CountryName = (string ) Reader["CountryName"];
               if (Reader["Code"] == DBNull.Value)
               {
                  Code = "";
               }
               else
               {
                  Code = (string)Reader["Code"];
               }
               if (Reader["PhoneCode"] == DBNull.Value)
               {
                  PhoneCode = "";
               }
               else
               {
                  PhoneCode = (string)Reader["PhoneCode"];
               }
            }
            Reader.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
            IsFound = false;
         }
         finally
         {
            connection.Close();
         }
         return IsFound;
      }

      public static bool GetCountryInfoByName(string CountryName, ref int CountryID, ref string Code, ref string PhoneCode)
      {
         bool IsFound = false;
         SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionStr);

         string query = "select * from Countries where CountryName = @CountryName";
         SqlCommand Command = new SqlCommand(query, connection);

         Command.Parameters.AddWithValue("@CountryName", CountryName);
         try
         {
            connection.Open();
            SqlDataReader Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
               IsFound = true;
               CountryID = (int)Reader["CountryID"];
               if (Reader["Code"] == DBNull.Value)
               {
                  Code = "";
               }
               else
               {
                  Code = (string)Reader["Code"];
               }
               if (Reader["PhoneCode"] == DBNull.Value)
               {
                  PhoneCode = "";
               }
               else
               {
                  PhoneCode = (string)Reader["PhoneCode"];
               }
            }
            Reader.Close();
         }
         catch (Exception ex)
         {
            Console.WriteLine("Error : " + ex.Message);
            IsFound = false;
         }
         finally
         {
            connection.Close();
         }

         return IsFound;
      }


   }
}

