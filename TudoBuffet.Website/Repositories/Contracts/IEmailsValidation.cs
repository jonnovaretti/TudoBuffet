using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IEmailsValidation
    {
        void Insert(EmailValidation emailValidation);
    }
}
