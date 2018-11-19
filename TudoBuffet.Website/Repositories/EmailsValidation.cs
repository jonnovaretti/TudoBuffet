using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class EmailsValidation : IEmailsValidation
    {
        private readonly MainDbContext mainDbContext;

        public EmailsValidation(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void Insert(EmailValidation emailValidation)
        {
            mainDbContext.Add(emailValidation);
            mainDbContext.SaveChanges();
        }
    }
}
