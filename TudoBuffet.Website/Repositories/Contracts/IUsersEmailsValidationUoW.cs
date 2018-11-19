using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public interface IUsersEmailsValidationUoW : IUnitOfWork<IUsersEmailsValidationUoW>
    {
        void Execute(User user, EmailValidation emailValidation);
    }
}
