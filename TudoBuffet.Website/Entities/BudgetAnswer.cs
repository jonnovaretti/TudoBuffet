using System;

namespace TudoBuffet.Website.Entities
{
    public class BudgetAnswer : BaseEntity
    {
        public BudgetQuestion Question { get; set; }
        public string Answer { get; set; }
    }
}
