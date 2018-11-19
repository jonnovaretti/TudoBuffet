using SendGrid;
using System;
using System.Linq;
using System.Net;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Services
{
    public class UserSignupService : IUserSignup
    {
        private readonly MainDbContext mainDbContext;
        private readonly IEmailSender emailSenderService;
        private readonly IUsersEmailsValidationUoW usersEmailsValidationUoW;

        public UserSignupService(MainDbContext mainDbContext, IEmailSender emailSenderService, IUsersEmailsValidationUoW usersEmailsValidationUoW)
        {
            this.mainDbContext = mainDbContext;
            this.emailSenderService = emailSenderService;
            this.usersEmailsValidationUoW = usersEmailsValidationUoW;
        }

        public void RegisterNewUser(User user)
        {
            user.Validate();

            if (!IsNewEmail(user))
                throw new BusinessException("Já existe um cadastro com esse e-mail");

            using (var unitOfWork = usersEmailsValidationUoW.BeginTransaction())
            {
                try
                {
                    EmailValidation emailValidation;
                    Response response;

                    user.Id = Guid.NewGuid();
                    user.IsActive = false;

                    emailValidation = EmailValidation.Build(user.Email);

                    unitOfWork.ExecuteInserts(user, emailValidation);

                    response = emailSenderService.SendEmailValidation(emailValidation);

                    if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Accepted)
                        unitOfWork.Commit();
                    else
                    {
                        unitOfWork.Rollback();
                        throw new Exception("Ocorreu um erro durante o cadastro. Tente novamente mais tarde");
                    }
                }
                catch (Exception)
                {
                    unitOfWork.Rollback();
                    throw;
                }
            }
        }

        private bool IsNewEmail(User user)
        {
            bool isNewEmail;

            isNewEmail = !mainDbContext.Users.Any(u => u.Email == user.Email);

            return isNewEmail;
        }
    }
}
