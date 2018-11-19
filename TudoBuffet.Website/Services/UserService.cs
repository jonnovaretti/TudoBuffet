using SendGrid;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Services
{
    public class UserService : IUserSignupService
    {
        private readonly MainDbContext mainDbContext;
        private readonly IEmailSenderService emailSenderService;
        private readonly IUsersEmailsValidationUoW usersEmailsValidationUoW;

        public UserService(MainDbContext mainDbContext, IEmailSenderService emailSenderService, IUsersEmailsValidationUoW usersEmailsValidationUoW)
        {
            this.mainDbContext = mainDbContext;
            this.emailSenderService = emailSenderService;
            this.usersEmailsValidationUoW = usersEmailsValidationUoW;
        }

        public void RegisterNewUser(User user)
        {
            ValidateUser(user);

            if (!IsNewEmail(user))
                throw new ValidationException("Já existe um cadastro com esse e-mail");

            using (var unitOfWork = usersEmailsValidationUoW.BeginTransaction())
            {
                try
                {
                    EmailValidation emailValidation;
                    Response response;

                    user.Id = Guid.NewGuid();
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

        private static void ValidateUser(User user)
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new ArgumentNullException("Campo obrigatório, e-mail não preenchido");

            if (!new EmailAddressAttribute().IsValid(user.Email))
                throw new FieldAccessException("E-mail inválido");

            if (string.IsNullOrEmpty(user.Name))
                throw new ArgumentNullException("Campo obrigatório, nome não preenchido");

            if (user.PasswordHash == 0)
                throw new ArgumentNullException("Campo obrigatório, senha não preenchido");
        }
    }
}
