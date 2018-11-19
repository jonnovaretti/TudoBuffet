using System;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Entities
{
    public class EmailValidation
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool WasValidate { get; set; }

        public static EmailValidation Build(string email)
        {
            EmailValidation emailValidation;
            string token;

            token = TextRandomGenerator.RandomString(120);

            emailValidation = new EmailValidation();
            emailValidation.Id = Guid.NewGuid();
            emailValidation.Email = email;
            emailValidation.Token = token;
            emailValidation.ExpireAt = DateTime.Now.AddHours(24);
            emailValidation.WasValidate = false;

            return emailValidation;
        }
    }
}
