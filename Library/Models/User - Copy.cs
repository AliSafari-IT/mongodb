using Library.Tools;
using MongoDB.Bson;
using System.Text.RegularExpressions;

namespace Library.Models
{
    public class User1
    {
        private string password;
        private string email;

        public ObjectId Id { get; set; }
        public string Fullname { get; set; }
        public string Email
        {
            get => email; set
            {
                if (EmailChecker(value))
                {
                    email = value;
                }
                else
                {
                    Console.WriteLine("Invalid Email Address");
                };
            }
        }

        public string Password
        {
            get => password; set
            {
                if (PasswordChecker(value))
                {
                    password = value;
                }
                else
                {
                    Console.WriteLine(" Password should be at least 8 characters long and should contain at least one uppercase letter, one lowercase letter and one number");
                }

            }
        }

        public global::System.Boolean UserInfoIsValid
        {
            get => new EmailValidator().IsValidEmail(Email)
        && PasswordChecker(Password);
            set
            { }
        }

        public bool PasswordChecker(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            if (!password.Any(char.IsUpper))
            {
                return false;
            }
            if (!password.Any(char.IsLower))
            {
                return false;
            }
            if (!password.Any(char.IsNumber))
            {
                return false;
            }
            return true;
        }

        public bool EmailChecker(string email)
        {
            // Check for valid email format using regular expression
        string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return false; // Invalid email format
            }

            // Check for invalid email domain
            string[] invalidDomains = { "example.com", "example.org", "example.net" };
            if (invalidDomains.Any(domain => email.Contains(domain)))
            {
                return false; // Invalid email domain
            }
            // Check for invalid domain address like: a_40e6f35e-a416-43b3-bb63-dc4d9721cee7@..com
            if (email.Contains("@.."))
            {
              //  return false; // Invalid domain address
            }

            return true; // Email is valid
        }

        public void ShowInfo(int Id, string Fullname, string Email)
        {
            Console.WriteLine($"{Id} {Fullname} {Email}");
        }

    }
}
