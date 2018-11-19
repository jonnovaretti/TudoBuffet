using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserSignup
    {
        void RegisterNewUser(User user);
    }
}
