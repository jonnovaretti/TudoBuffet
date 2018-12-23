using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
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

        public Buffet GetBuffetsById(Guid buffetId)
        {
            Buffet buffetFound;

            buffetFound = mainDbContext.Buffets.Include(b => b.Owner).Include(b => b.Photos).FirstOrDefault(b => b.Id == buffetId);

            return buffetFound;
        }

        public IEnumerable<Buffet> GetBuffetsByIds(List<string> buffetsIds)
        {
            IEnumerable<Buffet> buffetsFound;

            buffetsFound = from b in mainDbContext.Buffets.Include(b => b.Photos)
                           where buffetsIds.Contains(b.Id.ToString())
                           select b;

            return buffetsFound;
        }

        public List<Buffet> GetBuffetsHighlighWeek()
        {
            return mainDbContext.Buffets.Include(b => b.Photos).ToList();
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
            mainDbContext.Entry(buffet.Owner).State = EntityState.Detached;
            mainDbContext.SaveChanges();

            return buffet.Id;
        }

        public IEnumerable<Buffet> GetBuffets(string uf, string cidade, BuffetCategory? buffetCategory, BuffetEnvironment? buffetEnvironment, RangePrice? rangePrice, string name = null)
        {
            List<Buffet> buffets;
            StringBuilder where;
            List<object> paramsValue;
            int paramsOrder = 2;

            paramsValue = new List<object>();
            where = new StringBuilder();

            where.Append(" city == @0");
            paramsValue.Add(cidade);

            where.Append(" and ");
            where.Append(" state == @1");
            paramsValue.Add(uf);

            if (buffetCategory.HasValue)
            {
                where.Append(" and ");
                where.Append(" category == @" + paramsOrder);
                paramsValue.Add(buffetCategory);
                paramsOrder++;
            }

            if (buffetEnvironment.HasValue)
            {
                where.Append(" and ");
                where.Append(" environment = @" + paramsOrder);
                paramsValue.Add(buffetEnvironment);
                paramsOrder++;
            }

            if (buffetEnvironment.HasValue)
            {
                where.Append(" and ");
                where.Append(" price = @" + paramsOrder);
                paramsValue.Add(buffetEnvironment);
                paramsOrder++;
            }

            if (!string.IsNullOrEmpty(name))
            {
                where.Append(" and ");
                where.Append(" name = @" + paramsOrder);
                paramsValue.Add(name);
            }

            buffets = mainDbContext.Buffets.Include(b => b.Photos).Include(b => b.PlanSelected).Where(where.ToString(), paramsValue.ToArray()).OrderBy(b => b.PlanSelected.Order).ToList();

            return buffets;
        }

        public Guid Update(Guid id, Buffet buffet)
        {
            buffet.Id = id;

            mainDbContext.Update(buffet);

            mainDbContext.Entry(buffet.PlanSelected).State = EntityState.Detached;
            mainDbContext.Entry(buffet.Owner).State = EntityState.Detached;

            mainDbContext.SaveChanges();

            return buffet.Id;
        }
    }
}
