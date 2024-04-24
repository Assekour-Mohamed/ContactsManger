using System;
using System.Data;
using System.Diagnostics.Contracts;
using BusinessLayer;
namespace ContactsPresentationLayer
{
   internal class Program
   {
      static void testDeleteContactByCountryID(int Id)
      {
         if (clsContact.DeleteContactByCountryID(Id))
         {
            Console.WriteLine("Deleted success");
         }
         else
         {
            Console.WriteLine("Failed");
         }
      }

      
      static void testFindContact(int id)
      {
         clsContact Contact = clsContact.Find(id);
         if (Contact != null) 
         {
            Console.WriteLine(Contact.FirstName + " " + Contact.LastName);
            Console.WriteLine(Contact.Email);
            Console.WriteLine(Contact.Phone);
            Console.WriteLine(Contact.Address);
            Console.WriteLine(Contact.DateOfBirth);
            Console.WriteLine(Contact.CountryID);
            Console.WriteLine(Contact.ImagePath);
         }
         else
         {
            Console.WriteLine("Contact [" + id + "] Not found!");
         }

      }
      static void testAddNewContact()
      {
         clsContact Contact = new clsContact();

         Contact.ID = -1;
         Contact.FirstName = "ahemed";
         Contact.LastName = "ahar";
         Contact.Email = "hhor@gamil.com";
         Contact.Phone = "+212 4443333";
         Contact.Address = "casablacna morroro";
         Contact.CountryID = 1;
         Contact.ImagePath = @"https:\\google.\comimageMo";

         if (Contact.Save())
         {
            Console.WriteLine("Contact added successfully");
         }
         else
         {
            Console.WriteLine("Failed");
         }
      }
      static void testUpdateContact(int Id)
      {
         clsContact Contact = clsContact.Find(Id);
         if (Contact != null)
         {    
            Contact.ImagePath = @"https:\\google.com\Admed.png";

            if ( Contact.Save()) 
            {
               Console.WriteLine("Updated success"); 
            }
            else 
            { 
               Console.WriteLine("Failed to update"); 
            }

         }
         else
         {
            Console.WriteLine("no contact With this Id");
         }
      }
      static void testDeleteContact(int Id)
      {
         if (clsContact.isContactExist(Id))
         {
            if (clsContact.DeleteContact(Id))
            {
               Console.WriteLine("Deleted success");
            }
            else
            {
               Console.WriteLine("Failed");
            }
         }
         else
         {
            Console.WriteLine("Failed : Contact not exist");
         }
         
         
      }

      static void testListAllContacts()
      {
         DataTable dt = clsContact.GetAllContactsInfo(); 
         foreach (DataRow row in dt.Rows)
         {
            Console.WriteLine(row["ContactID"]);
         }
      }
      static void testIsContactExist(int Id)
      {
         if (clsContact.isContactExist(Id))
         {
            Console.WriteLine("contact exist");
         }
         else
         {
            Console.WriteLine("not exist");
         }
      }
      
      static void testFindCountry(int ID)
      {
         clsCountry country = clsCountry.Find(ID);
         if (country != null) 
         {
            Console.WriteLine("Id : " + country.CountryId);
            Console.WriteLine("Name : " + country.CountryName);
            Console.WriteLine("Code : " + country.Code);
            Console.WriteLine("Phone Code : " + country.PhoneCode);
         }
         else
         {
            Console.WriteLine("not found");
         }
      }
      static void testIsCountryExistName(string name)
      {
         if (clsCountry.IsCountryExistByName(name))
         {
            Console.WriteLine("Yes Exist");
         }
         else
         {
            Console.WriteLine("Not found");
         }

      }
      static void testIsCountryExistByID(int id)
      {
         if (clsCountry.IsCountryExistByID(id))
         {
            Console.WriteLine("Yes Exist");
         }
         else
         {
            Console.WriteLine("Not found");
         }

      }

      static void testUpdateCoutry(int Id)
      {
         clsCountry country = clsCountry.Find(Id);
         if (country != null)
         {
            country.CountryName = " World";
            country.Code = "ma";
            if (country.Save())
            {
               Console.WriteLine("updated success");
            }
            else
            {
               Console.WriteLine("Failed");
            }

         }
      }
      static void testAddCoutry()
      {
         clsCountry country = new clsCountry();

         country.CountryName = "Assekour World";
         country.Code = "";
         country.PhoneCode = "212";
         if (country.Save())
         {
            Console.WriteLine("added success");
         }
         else
         {
            Console.WriteLine("Failed");
         }


      }
      static void testDeleteCoutry(int id)
      {
         if (clsCountry.IsCountryExistByID(id))
         {
            if (clsCountry.DeleteCountry(id))
            {
               Console.WriteLine("dleted success");
            }
            else
            {
               Console.WriteLine("Failed");
            }

         }
         else
         {
            Console.WriteLine("not found");
         }


      }
      
      static void testGetAllCountriesInfo()
      {
         DataTable dt =  clsCountry.GetAllCountriesInfo();
         foreach (DataRow row in dt.Rows)
         {
            Console.WriteLine(row["CountryID"]);
         }
      }
      static void Main(string[] args)
      {

         //testFindContact(16);
         //testAddNewContact();
         //testUpdateContact(135);
         //testListAllContacts();
         //testDeleteContact(7);
         //testIsContactExist(144);

         //testFindCountry(10);
         //testIsCountryExist("United tates");
         //testUpdateCoutry(11);
         //testAddCoutry();
         //testDeleteCoutry("Assekour World");
         //testIsCountryExistByID(2);
         //testGetAllCountriesInfo();
         //testDeleteContactByCountryID(2);
         //testDeleteCoutry(10);
         
         Console.ReadKey();
      }
   }
}
