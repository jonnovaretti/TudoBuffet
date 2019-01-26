using Microsoft.EntityFrameworkCore;
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

        public Budget GetById(Guid budgetId)
        {
            var budgetsFound = (from b in mainDbContext.Budgets
                                join d in mainDbContext.BudgetDetails on b.Id equals d.BudgetId
                                join f in mainDbContext.Buffets on d.Buffet.Owner.Id equals f.Owner.Id
                                where budgetId.Equals(d.Id)
                                select b).Include(b => b.Details).ThenInclude(d => d.Questions).Include(b => b.PartyOwner).Include(b => b.Details).ThenInclude(d => d.Buffet).ThenInclude(b => b.Owner);

            return budgetsFound.FirstOrDefault();
        }

        public void Insert(Budget budget)
        {
            mainDbContext.Budgets.Add(budget);

            mainDbContext.Entry(budget.PartyOwner).State = EntityState.Detached;

            foreach (var detail in budget.Details)
            {
                mainDbContext.Entry(detail.Buffet).State = EntityState.Detached;
            }

            mainDbContext.SaveChanges();
        }

        public IEnumerable<Budget> GetByUserId(Guid userId)
        {
            return mainDbContext.Budgets.Include(b => b.Details).ThenInclude(d => d.Buffet).Where(b => b.PartyOwner.Id == userId);
        }

        public IEnumerable<Budget> GetByOwnerBuffet(Guid userId)
        {
            var budgetsFound = (from b in mainDbContext.Budgets
                                join d in mainDbContext.BudgetDetails on b.Id equals d.BudgetId
                                join f in mainDbContext.Buffets on d.Buffet.Owner.Id equals f.Owner.Id
                                where userId.Equals(f.Owner.Id)
                                select b).Include(b => b.Details).Include(b => b.PartyOwner);

            return budgetsFound.ToList();
        }
    }
}