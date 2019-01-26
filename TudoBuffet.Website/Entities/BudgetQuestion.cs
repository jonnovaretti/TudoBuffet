using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class BudgetQuestion : BaseEntity
    {
        public BudgetQuestion()
        {
            Answers = new List<BudgetAnswer>();
        }

        public string Question { get; set; }
        public List<BudgetAnswer> Answers { get; set; }
    }
}
