using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IBudgets
    {
        void Insert(Budget budget);
        IEnumerable<Budget> GetByUserId(Guid userId);
    }
}
