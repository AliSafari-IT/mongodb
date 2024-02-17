using Library.Enums;
using Library.MongoService;
using Library.Tools;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Library.Models
{
    public class User : IModel
    {
        public string password { get; private set; }
        private string email;

        // Fullname
        [BsonElement("fullname")]
        [JsonPropertyName("fullname")]
        [JsonProperty(PropertyName = "fullname")]
        public string Fullname { get; set; }

        [BsonElement("username")]
        [JsonPropertyName("username")]
        [JsonProperty(PropertyName = "username")]
        public string Username { get; set; }

        [BsonElement("email")]
        [JsonPropertyName("email")]
        [JsonProperty(PropertyName = "email")]
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
        [BsonIgnore]
        [JsonPropertyName("password")]
        [JsonProperty(PropertyName = "password")]
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
        [BsonElement("password_hash")]
        [JsonPropertyName("password_hash")]
        [JsonProperty(PropertyName = "password_hash")]
        public string PasswordHash
        {
            get => password;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    password = new PasswordHasher().HashPassword(value);
                    Console.WriteLine("pH: " + password);
                }
            }
        }

        [BsonElement("email_verified")]
        [JsonPropertyName("email_verified")]
        [JsonProperty(PropertyName = "email_verified")]
        public bool? EmailVerified { get; set; }

        [BsonElement("last_login_date")]
        [JsonPropertyName("last_login_date")]
        [JsonProperty(PropertyName = "last_login_date")]
        public DateTime? LastLoginDate { get; set; }

        [BsonIgnore]
        [JsonPropertyName("access_token")]
        [JsonProperty(PropertyName = "access_token")]
        public string Token { get; set; }

        [BsonElement("role")]
        [JsonPropertyName("role")]
        [JsonProperty(PropertyName = "role")]
        public UserRoleEnum Role { get; set; }

        [BsonElement("gender")]
        [JsonPropertyName("gender")]
        [JsonProperty(PropertyName = "gender")]
        public UserGenderEnum Gender { get; set; }

        [BsonElement("avatar")]
        [JsonPropertyName("avatar")]
        [JsonProperty(PropertyName = "avatar")]
        public string Avatar { get; set; }

        [BsonElement("avatar_tracker")]
        [JsonPropertyName("avatar_tracker")]
        [JsonProperty(PropertyName = "avatar_tracker")]
        public string AvatarTracker { get; set; }

        [BsonElement("localization")]
        [JsonPropertyName("localization")]
        [JsonProperty(PropertyName = "localization")]
        public string Localization { get; set; }


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

        public void HideConfindentialValues()
        {
            this.Password = null;
            this.PasswordHash = null;
            this.Email = null;
            this.EmailVerified = null;
            this.LastLoginDate = null;
            this.Token = null;
            this.Localization = null;

        }
    }
}
