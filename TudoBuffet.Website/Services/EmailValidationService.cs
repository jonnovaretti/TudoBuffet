using System;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Services
{
    public class EmailValidationService : IEmailValidatorService
    {
        private readonly MainDbContext mainDbContext;

        public EmailValidationService(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void ActiveEmail(string token)
        {
            EmailValidation emailValidation;
            User user;

            emailValidation = mainDbContext.EmailsValidation.FirstOrDefault(e => e.Token == token);
            emailValidation.Validate();

            user = mainDbContext.Users.FirstOrDefault(u => u.Email == emailValidation.Email);
            user.Active();

            mainDbContext.Attach(emailValidation);
            mainDbContext.Attach(user);
            mainDbContext.SaveChanges();
        }
    }
}
