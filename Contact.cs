using System;
using System.Data;
using DataAccessLayer;

namespace BusinessLayer
{
   public class clsContact
   {
      public enum enMode  { Update = 1 , AddNew = 2 };
      enMode Mode = enMode.AddNew;
      public int ID { set; get; }
      public string FirstName { set; get; }
      public string LastName { set; get; }
      public string Email { set; get; }
      public string Phone { set; get; }
      public string Address { set; get; }
      public DateTime DateOfBirth { set; get; }
      public string ImagePath { set; get; }

      public int CountryID { set; get; }
      public clsContact()
      {
         this.ID = -1;
         this.FirstName = "";
         this.LastName = "";
         this.Email = "";
         this.Phone = "";
         this.Address = "";
         this.DateOfBirth = DateTime.Now;
         this.ImagePath = "";
         this.CountryID = -1;

         Mode = enMode.AddNew;

      }
      private clsContact(int Id , string FirstName , string LastName , string Email, string Phone , string Address ,  DateTime DateOfBirth, int CountryID ,string ImagePath)
      {
         this.ID = Id;
         this.FirstName = FirstName;
         this.LastName = LastName;
         this.Email = Email;
         this.Phone = Phone;
         this.Address = Address;
         this.DateOfBirth = DateOfBirth;
         this.ImagePath = ImagePath;
         this.CountryID = CountryID;

         Mode = enMode.Update;
      }
      public static clsContact Find(int id)
      {
         string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
         DateTime DateOfBirth = DateTime.Now;
         int CountryID = -1;

         if (clsContactsData.GetContactInfoByID(id, ref FirstName, ref LastName,
                        ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
         {
            return new clsContact(id, FirstName, LastName,
                        Email, Phone, Address, DateOfBirth, CountryID, ImagePath);
         }
         else
         {
            return null;
         }
      }

      public static bool DeleteContact(int id)
      {
         return clsContactsData.DeleteContact(id);
      }
      public static bool DeleteContactByCountryID(int id)
      {
         return clsContactsData.DeleteContactByCountryID(id);
      }

      public static bool isContactExist(int id)
      {
         return clsContactsData.isContactExist(id);
      }
      public static DataTable GetAllContactsInfo( )
      {
         return clsContactsData.GetAllContactsInfo();
      }
      private bool _AddNewContact()
      {
         this.ID = clsContactsData.AddContact(this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
         return this.ID != -1;
      }
      private bool _UpdateContact()
      {
         return clsContactsData.UpdateContact(this.ID, this.FirstName, this.LastName, this.Email, this.Phone, this.Address, this.DateOfBirth, this.CountryID, this.ImagePath);
      }
      public bool Save()
      {
         switch (Mode)
         {
            case enMode.AddNew:        
               if (_AddNewContact())
               {
                  Mode = enMode.Update;
                  return true;
               }
               else
               {
                  return false;
               }            
            case enMode.Update:
              
               return _UpdateContact();
         }
         return false;
      }
   }
}
