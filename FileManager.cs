using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace PhoneBook.DAL
{
    public class FileManager
    {
        public IEnumerable<Contact> Load(string filePath)
        {
            List<Contact> contacts = new();
            using BinaryReader reader = new(File.Open(filePath, FileMode.Open));
            while (reader.PeekChar() > -1)
            {
                Contact contact = new(reader.ReadInt32())
                {
                    FirstName = reader.ReadString(),
                    LastName = reader.ReadString(),
                    Phone = reader.ReadString(),
                    Email = reader.ReadString(),
                };
                contacts.Add(contact);
            }
            return contacts;
            //waikitxos mititebuli failidan da daabrunos mititebuli obiektebis siad.
            //throw new NotImplementedException();

        }

        public void Save(string filePath, IEnumerable<Contact> contacts)
        {
            CheckId(contacts);
            using BinaryWriter writer = new(File.Open(filePath, FileMode.Create));
            foreach (var item in contacts)
            {
                writer.Write(item.ID);
                writer.Write(item.FirstName);
                writer.Write(item.LastName);
                writer.Write(item.Phone);
                writer.Write(item.Email);
            }

            //chaweros migebuli kontaktebis sia mititebul failshi binalurad.
            //ar dagaviwkdet chawerisas id-ze unikalurebis 
            //throw new NotImplementedException();
        }

        private void CheckId(IEnumerable<Contact> contacts)
        {
            HashSet<int> idCheck = new();

            foreach (Contact contact in contacts)
            {
                if (idCheck.Contains(contact.ID))
                {
                    throw new Exception("contacts ID is not unique.");
                }
                idCheck.Add(contact.ID);
            }
        }
    }
}
