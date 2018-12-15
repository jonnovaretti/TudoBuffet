using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class UsersEmailsValidationUoW : IUsersEmailsValidationUoW
    {
        private readonly MainDbContext mainDbContext;

        public UsersEmailsValidationUoW(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void Commit()
        {
            mainDbContext.Database.CommitTransaction();
        }

        public IUsersEmailsValidationUoW BeginTransaction()
        {
            mainDbContext.Database.BeginTransaction();
            return this;
        }

        public void Rollback()
        {
            mainDbContext.Database.RollbackTransaction();
        }
        
        public void Dispose()
        {
            mainDbContext.Dispose();
        }

        public void ExecuteUserInsert(User user)
        {
            mainDbContext.Add(user);
            mainDbContext.SaveChanges();
        }

        public void ExecuteEmailValidationInsert(EmailValidation emailValidation)
        {
            mainDbContext.Add(emailValidation);
            mainDbContext.SaveChanges();
        }
    }
}
