using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class BudgetDetail : BaseEntity
    {
        public Buffet Buffet { get; set; }
        public bool? IsDateAvaliable { get; set; }
        public DateTime? ProposedDateFor { get; set; }
        public DateTime? AnsweredAt { get; set; }
        public List<BudgetQuestion> Questions { get; set; }
        public Guid BudgetId { get; set; }
    }
}
