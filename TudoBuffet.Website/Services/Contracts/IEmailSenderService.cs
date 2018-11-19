using SendGrid;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IEmailSenderService
    {
        Response SendEmailValidation(EmailValidation emailValidation);
    }
}
