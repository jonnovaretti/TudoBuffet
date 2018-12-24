using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Buffet>> GetBuffets(string uf, string city, BuffetCategory? buffetCategory, BuffetEnvironment? buffetEnvironment, RangePrice? rangePrice, string name = null)
        {
            ConditionBuffetBuilder conditionBuffet;
            List<Buffet> buffets;

            conditionBuffet = new ConditionBuffetBuilder().WhereCity(city)
                                                          .WhereUf(uf)
                                                          .WhereCategory(buffetCategory)
                                                          .WhereEnvironment(buffetEnvironment)
                                                          .WhereRangePrice(rangePrice)
                                                          .WhereName(name)
                                                          .Build();

            buffets = await mainDbContext.Buffets.Include(b => b.Photos)
                                                 .Include(b => b.PlanSelected)
                                                 .Where(conditionBuffet.GetWhere(), conditionBuffet.GetParams())
                                                 .OrderBy(b => b.PlanSelected.Order)
                                                 .ToListAsync();
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
