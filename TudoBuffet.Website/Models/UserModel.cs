using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Models
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }

        public User ToEntity()
        {
            User user = new User();
            string salt, hashedPassword;

            salt = PasswordHashGenerator.GenerateSalt();
            hashedPassword = PasswordHashGenerator.CreateHashedTextFromText(Password, salt);

            user.Name = Name;
            user.Salt = salt;
            user.PasswordHash = hashedPassword;
            user.Email = Email;

            return user;
        }
    }
}
