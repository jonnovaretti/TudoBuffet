using System;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserAuthenticatior
    {
        User GetUserById(Guid id);
        bool IsCredentialCorrect(string email, string password);
        AuthenticatedUserModel GenerateJwt(string email);
    }
}
