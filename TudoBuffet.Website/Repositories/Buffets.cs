using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class Buffets : IBuffets
    {
        private readonly MainDbContext mainDbContext;

        public Buffets(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public Buffet GetBuffetsById(string buffetId)
        {
            Buffet buffetFound;

            buffetFound = mainDbContext.Buffets.FirstOrDefault(b => b.Id == Guid.Parse(buffetId));

            return buffetFound;
        }

        public IEnumerable<Buffet> GetBuffetsFromUserId(Guid userId)
        {
            return mainDbContext.Buffets.Include(b => b.PlanSelected)
                                        .Where(b => b.Owner.Id == userId)
                                        .DefaultIfEmpty();
        }

        public Guid Save(Buffet buffet)
        {
            mainDbContext.Add(buffet);
            mainDbContext.Entry(buffet.PlanSelected).State = EntityState.Detached;
            mainDbContext.SaveChanges();

            return buffet.Id;
        }
    }
}
