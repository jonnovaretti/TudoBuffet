using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IUsersEmailsValidationUoW : IUnitOfWork<IUsersEmailsValidationUoW>
    {
        void ExecuteUserInsert(User user);
        void ExecuteEmailValidationInsert(EmailValidation emailValidation);
    }
}
