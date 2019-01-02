using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class Budgets : IBudgets
    {
        private readonly MainDbContext mainDbContext;

        public Budgets(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public void Insert(Budget budget)
        {
            mainDbContext.Budgets.Add(budget);
            mainDbContext.SaveChanges();
        }

        public IEnumerable<Budget> GetByUserId(Guid userId)
        {
            return mainDbContext.Budgets.Where(b => b.PartyOwner.Id == userId);
        }
    }
}
