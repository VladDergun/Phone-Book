namespace PhoneBook
{
    public class Contacts
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Number { get; set; }
        private Guid id { get; set; }
        public string ID { get; set; }

        public Contacts(string firstName, string lastName, string number)
        {
            id = Guid.NewGuid();
            ID = id.ToString();
            FirstName = firstName;
            LastName = lastName;
            Number = number;
        }
        public Contacts(string name, string number)
        {
            id = Guid.NewGuid();
            ID = id.ToString();
            FirstName = name;
            LastName = "";
            Number = number;
        }
        public Contacts(string number)
        {

            id = Guid.NewGuid();
            ID = id.ToString();
            Number = number;
            FirstName = "";
            LastName = "";
        }
        public Contacts()
        {

        }
    }
}
