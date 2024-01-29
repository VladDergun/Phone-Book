using System.Text.Json;
using System.Text.RegularExpressions;

namespace PhoneBook
{
    public class Data
    {
        private static string jsonPath = "data\\database.json";
        private static string json = File.ReadAllText(jsonPath);

        private static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true // Set indentation to make the JSON readable
        };


        public static List<Contacts> GetContacts()
        {
            json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<List<Contacts>>(json);

        }

        public static void RemoveContacts(string id)
        {
            List<Contacts> contacts = GetContacts();

            Contacts contactToRemove = contacts.FirstOrDefault(cont => cont.ID == id);
            if (contactToRemove != null)
            {
                contacts.Remove(contactToRemove);

            }
            SaveContacts(contacts);


        }
        public static void AddContacts(string firstName, string lastName, string number)
        {


            Contacts newContact = null;
            List<Contacts> contacts = GetContacts();

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                newContact = new Contacts(firstName + lastName, number);
            }
            else
            {
                newContact = new Contacts(firstName, lastName, number);
            }
            if (newContact != null)
            {
                contacts.Add(newContact);

                SaveContacts(contacts);

            }

        }

        public static List<Contacts> FindContacts(string searchTerm)
        {
            List<Contacts> contacts = GetContacts();
            StringComparison comparison = StringComparison.OrdinalIgnoreCase;
            string[] searchTermArray = searchTerm.Split(' ');

            string stringWithOneSpace = Regex.Replace(searchTerm, @"\s+", " ").Trim();

            return contacts
                .Where(c =>
                    c.Number.StartsWith(stringWithOneSpace, comparison) ||
                    searchTermArray.All(word =>
                        (c.FirstName + " " + c.LastName).Split(' ').Contains(word) ||
                        (c.FirstName + " " + c.LastName).Split(' ').Any(a => a.StartsWith(word, comparison)))
                ).ToList();
        }
        public static Contacts GetContactInfo(string contactID)
        {
            List<Contacts> contactList = GetContacts();
            return contactList.FirstOrDefault(c => c.ID.Equals(contactID));
        }
        public static void EditContact(string contactID, string firstName, string lastName, string number)
        {

            List<Contacts> contactList = GetContacts();


            Contacts contact = new Contacts(firstName, lastName, number);
            int index = contactList.FindIndex(c => c.ID.Equals(contactID));

            if (index != -1)
            {
                contactList[index] = contact;
            }
            SaveContacts(contactList);


        }

        private static void SaveContacts(List<Contacts> contacts)
        {
            string updatedContactsJson = JsonSerializer.Serialize(contacts, jsonOptions);
            File.WriteAllText(jsonPath, updatedContactsJson);
        }

    }
}
