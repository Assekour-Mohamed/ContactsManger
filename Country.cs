using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
   public class clsCountry
   {
      public enum enMode { Update = 1, AddNew = 2 };
      enMode Mode = enMode.Update;

      public int CountryId { get; set; }
      public string CountryName { get; set; }
      public string Code { get; set; }
      public string PhoneCode {  get; set; } 


      private clsCountry(int CountryId, string CountryName, string Code, string PhoneCode)
      {
         this.CountryId = CountryId;
         this.CountryName = CountryName;
         this.Code = Code;
         this.PhoneCode = PhoneCode;
         Mode = enMode.Update;
      }
      public clsCountry()
      {
         this.CountryId = -1;
         this.CountryName = "";
         this.Code = "";
         this.PhoneCode = "";
         Mode = enMode.AddNew;
      }

      public static clsCountry Find(int CountryId)
      {
         string CountryName = "", Code = "", PhoneCode = "";
         if (clsCountriesData.GetCountryInfoByID(CountryId, ref CountryName, ref Code, ref PhoneCode))
         {
            return new clsCountry(CountryId, CountryName, Code, PhoneCode);
         }
         else
         {
            return null;
         }
      }
      public static clsCountry Find(string CountryName)
      {

         int ID = -1;
         string Code = "", PhoneCode = "";


         if (clsCountriesData.GetCountryInfoByName(CountryName, ref ID, ref Code, ref PhoneCode))

            return new clsCountry(ID, CountryName, Code, PhoneCode);
         else
            return null;

      }

      public static bool IsCountryExistByName(string CountryName)
      {
         return clsCountriesData.IsCountryExist(CountryName);
      }
      public static bool IsCountryExistByID(int id)
      {
         return clsCountriesData.IsCountryExist(id);
      }
      private bool _UpdateCountryInfo()
      {
         return (clsCountriesData.UpdateCountry(this.CountryId, this.CountryName,this.Code, this.PhoneCode));
      }
      private static bool DeleteAllContactsWithThisCountryID(int CountryID)
      {
         return clsContact.DeleteContactByCountryID(CountryID);
      }
      public static bool DeleteCountry(int id)
      {
         DeleteAllContactsWithThisCountryID(id);
         return clsCountriesData.DeleteCountry(id);        
      }
      private bool _AddNewCountry()
      {
         this.CountryId = clsCountriesData.AddCountry(this.CountryName, this.Code, this.PhoneCode);
         return CountryId != -1;
      }
      public static DataTable GetAllCountriesInfo()
      {
         return clsCountriesData.GetAllCountriesInfo();
      }
      public bool Save()
      {
         switch (Mode)
         {
            case enMode.Update:
               return _UpdateCountryInfo();
            case enMode.AddNew:
               if (_AddNewCountry())
               {
                  Mode = enMode.Update;
                  return true;
               }
             return false;
         }
         return false;
      }
   }
}