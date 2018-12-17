using System.Security.Claims;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserAccount
    {
        void RegisterNewUser(User user);
        ClaimsIdentity AutheticateUser(UserModel userModel);
    }
}
