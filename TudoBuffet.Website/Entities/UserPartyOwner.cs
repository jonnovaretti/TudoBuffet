using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class UserPartyOwner : User
    {
        public List<Budget> Budgets { get; set; }
    }
}
