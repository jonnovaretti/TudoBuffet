using System.Linq;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class Plans : IPlans
    {
        private readonly MainDbContext mainDbContext;

        public Plans(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public IEnumerable<Plan> GetAll()
        {
            return mainDbContext.Plans.ToList().OrderBy(o => o.Order);
        }
    }
}
