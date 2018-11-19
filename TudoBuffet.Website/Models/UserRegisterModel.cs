using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class UserRegisterModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmationPassword { get; set; }

        public User ToEntity()
        {
            User user = new User();
            user.Name = Name;
            user.PasswordHash = Password.GetHashCode();
            user.Email = Email;

            return user;
        }
    }
}
