using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly MainDbContext mainDbContext;
        private readonly IOptions<ConnectionString> connectionStringConfig;

        public EmailSenderService(MainDbContext mainDbContext, IOptions<ConnectionString> connectionStringConfig)
        {
            this.mainDbContext = mainDbContext;
            this.connectionStringConfig = connectionStringConfig;
        }

        public Response SendEmailValidation(EmailValidation emailValidation)
        {
            Response response;
            string url, body;

            url = "/confirmacaoemail?token=" + emailValidation.Token;
            body = EmailTemplateGenerator.GetEmailConfirmationTemplate(url);

            response = SendEmail(emailValidation, body, "Confirmação de e-mail");

            return response;
        }

        private Response SendEmail(EmailValidation emailValidation, string body, string subject)
        {
            var client = new SendGridClient(connectionStringConfig.Value.ApiKeySendgrid);
            var from = new EmailAddress("jonn.novaretti@gmail.com", "Tudo Buffet");
            var to = new EmailAddress(emailValidation.Email);
            var msg = MailHelper.CreateSingleEmail(from, to, subject, string.Empty, body);

            var response = client.SendEmailAsync(msg).GetAwaiter().GetResult();
            return response;
        }
    }
}
