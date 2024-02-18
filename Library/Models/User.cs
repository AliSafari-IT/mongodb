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
        public string password { get; set; }
        private string email;
        private bool hide;

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
                if (new EmailValidator().IsValidEmail(value))
                {
                    email = value;
                }
                else
                {
                    if(!hide) {
                        Console.WriteLine("Invalid Email Address");
                    }
                };
            }
        }
        [BsonIgnore]
        [JsonPropertyName("password")]
        [JsonProperty(PropertyName = "password")]
        public string Password { get => GetPasswordHash(); set => SetPasswordHash(value); }

        public global::System.String GetPasswordHash()
        {
            return password;
        }

        public void SetPasswordHash(global::System.String value)
        {
            password = value;
            if (value != null && value.Length > 0 && PasswordChecker(value))
            {
                password = new PasswordHasher().HashPassword(value);
                Console.WriteLine("ps: " + value + "\tpH: " + password);
            }
            else
            {
                if (!hide)
                {
                    Console.WriteLine("pH: " + value);
                    Console.WriteLine(" Password should be at least 8 characters long and should contain at least one uppercase letter, one lowercase letter and one number");
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


        public bool UserInfoIsValid
        {
            get => new EmailValidator().IsValidEmail(Email)
        && PasswordChecker(Password);
            set
            { }

        }


        public bool PasswordChecker(string password)
        {
            if (password == null)
            {
                return false; // Or throw an appropriate exception
            }
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

        public void HideConfindentialValues()
        {
            hide = true;
            this.Password = null;
            this.Email = null;
            this.EmailVerified = null;
            this.LastLoginDate = null;
            this.Token = null;
            this.Localization = null;

        }

        public void ShowInfo(User newUser)
        {
            Console.WriteLine($"Id: {Id} \nFullname: {Fullname} \nEmail: {Email} \nPassword: {Password ?? "(not set)"}  \nUserInfoIsValid: {UserInfoIsValid} \nDone!");
        }


    }
}
