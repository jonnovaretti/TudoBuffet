using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserSignupService
    {
        void RegisterNewUser(User user);
    }
}
