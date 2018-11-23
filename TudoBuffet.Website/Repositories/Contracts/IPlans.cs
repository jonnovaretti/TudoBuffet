using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IPlans
    {
        IEnumerable<Plan> GetAll();
    }
}
