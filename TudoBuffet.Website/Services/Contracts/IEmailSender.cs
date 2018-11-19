using SendGrid;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IEmailSender
    {
        Response SendEmailValidation(EmailValidation emailValidation);
    }
}
