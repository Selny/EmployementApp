using System.Collections;
using System.Collections.Generic;

namespace EmployementApp
{
    class User : IEnumerable
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool AccountType { get; set; } //0 -> Worker, 1 -> Require
        public bool Applied = false;

        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Profession { get; set; }
        public int Experience { get; set; }

        public override string ToString()
        {
            return $"Name: {Name}\nSurname: {Surname}\nAge: {Age}\nProfession: {Profession}\nExperience year: {Experience}";
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Username).GetEnumerator();
        }
    }

    class Job : IEnumerable
    {
        public string Company { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int RequiredExperience { get; set; }
        public int Salary { get; set; }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)Title).GetEnumerator();
        }

        public override string ToString()
        {
            return $"| Company: {Company}\n| Title: {Title}\n| Description: {Description}\n| Required experience: {RequiredExperience} years\n| \n| Salary: ${Salary}";
        }
    }

    static class Database
    {
        static public List<User> users = new()
        {
            new User
            {
                Username = "admin",
                Password = "admin",
                AccountType = true,
                Name = "Lucy", Surname = "Greenyard", Age = 45, Profession = "Marketing Manager", Experience = 4
            },
            new User
            {
                Username = "tural777",
                Password = "stepit",
                AccountType = false,
                Name = "Tural", Surname = "Novruzov", Age = 28, Profession = "Middle Developer, Student of Ismayil", Experience = 6
            }
        };
        static public List<Job> jobs = new()
        {
            new Job
            {
                Company = "Google",
                Title = "Machine Learning",
                Description = "We need senior AI developers for our new game project!",
                RequiredExperience = 4, Salary = 4400
            },
            new Job
            {
                Company = "AnonymBruh",
                Title = "Cyber Cecurity engineer",
                Description = "We need an engineer that protects server against attacks.",
                RequiredExperience = 2,
                Salary = 1200
            },
            new Job
            {
                Company = "StepIT",
                Title = "Junior Academy Teacher",
                Description = "Start you career with our new openings for teachers!",
                RequiredExperience = 0,
                Salary = 400
            },
            new Job
            {
                Company = "VR Studio",
                Title = "3D Modeller",
                Description = "We hire 3D modellers for our new VR game!",
                RequiredExperience = 3,
                Salary = 2600
            },
            new Job
            {
                Company = "100 Architecs",
                Title = "Interior Designer",
                Description = "Lets build fun stuff together! Looking forward to see your entries :)",
                RequiredExperience = 0,
                Salary = 1800
            }
        };
    }
}