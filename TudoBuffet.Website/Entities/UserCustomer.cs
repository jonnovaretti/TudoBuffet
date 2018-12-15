using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class UserCustomer : User
    {
        public List<Budget> Budgets { get; set; }
    }
}
