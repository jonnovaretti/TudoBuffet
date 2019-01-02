using Microsoft.AspNetCore.Authentication.Cookies;
using SendGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.Tools;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Services
{
    public class UserAccountService : IUserAccount
    {
        private readonly MainDbContext mainDbContext;
        private readonly IEmailSender emailSenderService;
        private readonly IUsersEmailsValidationUoW usersEmailsValidationUoW;

        public UserAccountService(MainDbContext mainDbContext, IEmailSender emailSenderService, IUsersEmailsValidationUoW usersEmailsValidationUoW)
        {
            this.mainDbContext = mainDbContext;
            this.emailSenderService = emailSenderService;
            this.usersEmailsValidationUoW = usersEmailsValidationUoW;
        }

        public ClaimsIdentity AutheticateUser(UserModel userModel)
        {
            if (IsCredentialCorrect(userModel.Email, userModel.Password))
            {
                User userFound;
                ClaimsIdentity claimsIdentity;

                userFound = mainDbContext.Users.Where(u => u.Email == userModel.Email).FirstOrDefault();

                if (userFound != null)
                {

                    var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name.ToString(), userFound.Email),
                            new Claim("fullname", userFound.Name),
                            new Claim("id", userFound.Id.ToString()),
                            new Claim(ClaimTypes.Role, Enum.GetName(typeof(Profile), userFound.Profile))
                        };

                    claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    return claimsIdentity;
                }
            }

            return null;
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

                    user.SetNewSignup();

                    emailValidation = EmailValidation.Build(user.Email);

                    unitOfWork.ExecuteUserInsert(user);
                    unitOfWork.ExecuteEmailValidationInsert(emailValidation);  

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

        private bool IsCredentialCorrect(string email, string password)
        {
            User user;
            string passwordHash;

            user = mainDbContext.Users.FirstOrDefault(u => u.Email == email);

            if (user != null)
            {
                passwordHash = PasswordHashGenerator.CreateHashedTextFromText(password, user.Salt);

                return user.PasswordHash.Equals(passwordHash);
            }

            return false;
        }

        private bool IsNewEmail(User user)
        {
            bool isNewEmail;

            isNewEmail = !mainDbContext.Users.Any(u => u.Email == user.Email);

            return isNewEmail;
        }
    }
}
