
using PhoneBook.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        }

        private void CheckId(IEnumerable<Contact> contacts)
        {
            HashSet<int> idCheck = new();

            foreach (Contact contact in contacts)
            {
                if (!idCheck.Add(contact.ID))
                {
                    throw new Exception("contacts ID is not unique.");
                }
            }
        }
    }
}
