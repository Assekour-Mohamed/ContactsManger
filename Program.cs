using System;
using System.Data;
using System.Diagnostics.Contracts;
using ContactsBusinessLayer;
namespace ContactsPresentationLayer
{
   internal class Program
   {  
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
      static void Main(string[] args)
      {

         //testFindContact(16);
         //testAddNewContact();
         //testUpdateContact(135);
         //testListAllContacts();
         testDeleteContact(7);
         //testIsContactExist(144);
         Console.ReadKey();
      }
   }
}
