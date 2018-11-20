using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserAuthenticatior
    {
        User GetUserById(Guid id);
        bool IsCredentialCorrect(string email, string password);
        string GenerateJwt(string email);
    }
}
