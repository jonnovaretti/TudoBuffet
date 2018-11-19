using System;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserAuthenticatior
    {
        bool IsCredentialCorrect(string email, string password);
        string GenerateJwt(string email);
    }
}
