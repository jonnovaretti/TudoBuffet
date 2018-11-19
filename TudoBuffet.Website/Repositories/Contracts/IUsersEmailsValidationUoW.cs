using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IUsersEmailsValidationUoW : IUnitOfWork<IUsersEmailsValidationUoW>
    {
        void ExecuteInserts(User user, EmailValidation emailValidation);
    }
}
