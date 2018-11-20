using System;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Entities
{
    public class EmailValidation : BaseEntity
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public bool WasValidate { get; set; }
        public DateTime? ValidateAt { get; set; }

        public static EmailValidation Build(string email)
        {
            EmailValidation emailValidation;
            string token;

            token = TextRandomGenerator.RandomString(120);

            emailValidation = new EmailValidation();
            emailValidation.Id = Guid.NewGuid();
            emailValidation.Email = email;
            emailValidation.Token = token;
            emailValidation.ExpireAt = DateTime.UtcNow.AddHours(24);
            emailValidation.WasValidate = false;

            return emailValidation;
        }
        
        public void Validate()
        {
            if (ExpireAt < DateTime.UtcNow)
                throw new BusinessException("Essa confirmação de e-mail está expirada");

            if (WasValidate)
                throw new BusinessException("Confirmação de e-mail previamente validada");

            WasValidate = true;
            ValidateAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
