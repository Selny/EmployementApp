using System;
using System.IO;

namespace EmployementApp
{
    static class Page
    {
        public static int CurrenPageIndex = -1;

        public static int Pointer = 0;
        public static int Limit = 10;
        public static bool isRunning = true;
        public static bool[] Select = new bool[10] {true, false, false, false, false, false, false, false, false, false};

        public static string Username;
        public static string Password;

        private static void Print(string txt, bool selected = false, bool format = true)
        {
            if(format) 
            {
                Limit++;
                Console.Write(selected ? "-> " : "   ");
            }
            Console.WriteLine(txt);
        }
        public static void Control()
        {
            var input = Console.ReadKey().Key;
            if (input == ConsoleKey.Spacebar)
            {
                CurrenPageIndex = -1;
                CurrentPage();
            }
            else if (input == ConsoleKey.UpArrow && Pointer > 0)
            {
                Select[Pointer] = false;
                Select[--Pointer] = true;
            }
            else if (input == ConsoleKey.DownArrow && Pointer < Limit - 1)
            {
                Select[Pointer] = false;
                Select[++Pointer] = true;
            }
            else if (input == ConsoleKey.LeftArrow)
            {
                if (CurrenPageIndex == -1)
                {
                    Console.WriteLine("Bye!");
                    Pointer = -2;
                    isRunning = false;
                }
                if(CurrenPageIndex == 1 || CurrenPageIndex == 2) Pointer = 0;
                CurrentPage();
            }
            else if (input == ConsoleKey.RightArrow)
            {
                if (CurrenPageIndex == 2)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (Select[i] == true)
                        {
                            Console.WriteLine();
                            Console.WriteLine(Database.jobs[i]);
                            var c = Console.ReadKey().Key;
                            if(c == ConsoleKey.Enter)
                            {
                                foreach (var user in Database.users)
                                {
                                    if (Username == user.Username)
                                    {
                                        Database.jobs[i].Description += "\n >>> Applied: " + user.Name + " " + user.Surname;
                                        user.Applied = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if(CurrenPageIndex == 3)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (Select[i] == true)
                        {
                            if (i == 0)
                            {
                                foreach (var job in Database.jobs)
                                {
                                    Console.WriteLine(job);
                                    Console.WriteLine();
                                }
                                Console.ReadKey();
                            }
                            else if (i == 1)
                            {
                                Console.WriteLine();
                                foreach (var user in Database.users)
                                {
                                    if(user.Applied) Console.WriteLine(user);
                                }
                                Console.ReadKey();
                            }
                            else if (i == 2)
                            {
                                string company, title, description;
                                int requiredExperience, salary;
                                Console.Write("Company name: ");
                                company = Console.ReadLine();
                                Console.Write("Job title: ");
                                title = Console.ReadLine();
                                Console.Write("Description: ");
                                description = Console.ReadLine();
                                Console.Write("Required experience: ");
                                requiredExperience = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Salary: ");
                                salary = Convert.ToInt32(Console.ReadLine());

                                Database.jobs.Add(new Job { Company = company, Title = title, Description = description, RequiredExperience = requiredExperience, Salary = salary });
                            }
                            else if (i == 3)
                            {
                                bool found = false;
                                Console.Write("Write job title: ");
                                string title = Console.ReadLine();
                                foreach (var job in Database.jobs)
                                {
                                    if(job.Title == title)
                                    {
                                        found = true;
                                        Database.jobs.Remove(job);
                                        break;
                                    }
                                }
                                if (!found)
                                {
                                    Console.WriteLine("Job not found!");
                                    Console.ReadKey();
                                }

                            }
                            else if (i == 4)
                            {
                                bool found = false;
                                Console.Write("Write job title: ");
                                string title = Console.ReadLine();
                                foreach (var job in Database.jobs)
                                {
                                    if (job.Title == title)
                                    {
                                        found = true;
                                        Console.Write("Update description: ");
                                        string newDescription = Console.ReadLine();
                                        Console.Write("Update required experience: ");
                                        int requiredExperience = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Update salary: ");
                                        int newSalary = Convert.ToInt32(Console.ReadLine());
                                        job.Description = newDescription;
                                        job.RequiredExperience = requiredExperience;
                                        job.Salary = newSalary;
                                    }
                                }
                                if (!found)
                                {
                                    Console.WriteLine("Job not found!");
                                    Console.ReadKey();
                                }
                            }
                        }
                    }
                }
                else
                {
                    CurrenPageIndex = Pointer;
                    CurrentPage();
                }
            }
            
        }
        public static void CurrentPage()
        {
            Console.Clear();
            switch (CurrenPageIndex)
            {
                case -1:
                    Greet();
                    if (Pointer == -2)
                    {
                        Console.Clear();
                        Console.WriteLine("Bye!");
                    }
                    break;
                case 0:
                    Login();
                    break;
                case 1:
                    Register();
                    break;
                case 2:
                    WorkerPanel();
                    break;
                case 3:
                    AdminPanel();
                    break;
            };
        }
        public static void Greet()
        {
            Limit = 0;
            Print("Welcome to the Employement App", format: false);
            Print("Log in", Select[0]);
            Print("Register", Select[1]);
        }
        public static void Login()
        {
            int access = 0;
            do
            {
                access = 0;
                Console.Write("Username: ");
                Username = Console.ReadLine();
                Console.Write("Password: ");
                Password = Console.ReadLine();

                try
                {
                    using (StreamReader reader = new StreamReader("database.txt"))
                    {
                        string line;
                        int i = 0;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (i % 2 == 0 && Username == line)
                            {
                                access++;
                            }
                            if (i % 2 == 1 && access == 1)
                            {
                                if (Password == line)
                                {
                                    access++;
                                    break;
                                }
                                else break;
                            }
                            i++;
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.Clear();
                if (access == 1)
                {
                    Console.WriteLine("Wrong password. Try again.");
                }
                else
                {
                    Console.WriteLine("User not found. Try again.");
                }
            } while (access != 2);
            foreach(var user in Database.users)
            {
                if(Username == user.Username)
                {
                    if(user.AccountType == false) CurrenPageIndex = 2;
                    if(user.AccountType == true) CurrenPageIndex = 3;
                }
            }
        }
        public static void Register()
        {
            bool notAvailable, accountType;
            string username, password, name, surname, profession;
            int age, experience;
            do
            {
                Console.Write("New username: ");
                username = Console.ReadLine();
                notAvailable = false;
                using (StreamReader reader = new StreamReader("database.txt"))
                {
                    string line;
                    int i = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (i % 2 == 0 && username == line)
                        {
                            notAvailable = true;
                            Console.Clear();
                            Console.WriteLine("Username is not available. Try again.");
                            break;
                        }
                        i++;
                    }
                }
            } while (notAvailable);
            Console.Write("New password: ");
            password = Console.ReadLine();
            using (StreamWriter sw = File.AppendText("database.txt"))
            {
                sw.WriteLine();
                sw.WriteLine(username);
                sw.Write(password);
            }
            Console.Write("Are you requirer? ");
            accountType = Convert.ToBoolean(Console.ReadLine());
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.Write("Enter your surname: ");
            surname = Console.ReadLine();
            Console.Write("How old are you: ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Your profession: ");
            profession = Console.ReadLine();
            Console.Write("Your experience: ");
            experience = Convert.ToInt32(Console.ReadLine());


            Database.users.Add(new User { Username = username, Password = password, AccountType = accountType, Name = name, Surname = surname, Age = age, Profession = profession, Experience = experience });

            CurrenPageIndex = -1;
        }

        public static void WorkerPanel()
        {
            int i = 0;
            Limit = 0;
            Console.WriteLine("Jobs:");
            foreach(var job in Database.jobs)
            {
                Print(job.Title, Select[i++]);
            }
        }
        public static void AdminPanel()
        {
            Limit = 0;
            Console.WriteLine("Admin Panel:");
            Print("Jobs", Select[0]);
            Print("Applied Users", Select[1]);
            Print("Add Job", Select[2]);
            Print("Remove Job", Select[3]);
            Print("Update Job", Select[4]);
        }
    }
}
