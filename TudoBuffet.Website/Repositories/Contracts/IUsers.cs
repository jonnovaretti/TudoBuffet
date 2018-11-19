using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IUsers
    {
        void Insert(User user);
    }
}
