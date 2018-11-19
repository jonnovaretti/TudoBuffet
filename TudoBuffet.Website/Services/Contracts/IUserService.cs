using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IUserService
    {
        void RegisterNewUser(User user);
        User GetUser(Guid id);
    }
}
