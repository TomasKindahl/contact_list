namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
            public void Print()
            {
                Console.WriteLine($"{persname} {surname}, {phone}, {address}, {birthdate}");
            }
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            PrintHelpMessage();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    // NYI: safe quit
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                // NYI: IMPORTANT
                else if (commandLine[0] == "list")
                {
                    foreach(Person p in contactList)
                    {
                        if(p != null)
                            p.Print();
                    }
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
                        LoadContactListFromFile(lastFileName);
                    }
                    else
                    {
                        lastFileName = commandLine[1]; // commandLine[1] is the first argument
                        // FIXME: Throws System.IO.FileNotFoundException: 
                        LoadContactListFromFile(lastFileName);
                    }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        SaveContactListToFile(lastFileName);
                    }
                    else
                    {
                        // NYI: Copy code from 'load' if-else branch
                        Console.WriteLine("Not yet implemented: save /file/");
                    }
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Console.Write("personal name: ");
                        string persname = Console.ReadLine();
                        Console.Write("surname: ");
                        string surname = Console.ReadLine();
                        Console.Write("phone: ");
                        string phone = Console.ReadLine();
                        // NYI: insert into contactList!!
                    }
                    else
                    {
                        // NYI!
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                }
                else if (commandLine[0] == "help")
                {
                    PrintHelpMessage();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
        }

        private static void SaveContactListToFile(string lastFileName)
        {
            using (StreamWriter outfile = new StreamWriter(lastFileName))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.persname}|{p.surname}|{p.phone}|{p.address}|{p.birthdate}");
                }
            }
        }

        private static void LoadContactListFromFile(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    LoadContact(line); // Also prints the line loaded
                }
            }
        }

        private static void LoadContact(string lineFromAddressFile)
        {
            Console.WriteLine(lineFromAddressFile); // INTE: 
            string[] attrs = lineFromAddressFile.Split('|');
            Person newPerson = new Person();
            newPerson.persname = attrs[0];
            newPerson.surname = attrs[1];
            string[] phones = attrs[2].Split(';'); 
            newPerson.phone = phones[0];      // FIXME: this line drops all phone numbers except the first
            // foreach(string p in phones)
            //    newPerson.phone.Add(phones[i])
            string[] addresses = attrs[3].Split(';');
            newPerson.address = addresses[0]; // FIXME: this line drops all phone numbers except the first
            for (int ix = 0; ix < contactList.Length; ix++)
            {
                if (contactList[ix] == null)
                {
                    contactList[ix] = newPerson;
                    break;
                }
            }
        }
        private static void PrintHelpMessage()
        {
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  delete       - emtpy the contact list");
            Console.WriteLine("  delete /persname/ /surname/ - delete a person");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
        }
    }
}
