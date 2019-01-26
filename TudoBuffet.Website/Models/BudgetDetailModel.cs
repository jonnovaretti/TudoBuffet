using System;

namespace TudoBuffet.Website.Models
{
    public class BudgetDetailModel
    {
        public Guid DetailId { get; set; }
        public string Title { get; set; }
        public string BuffetName { get; set; }
        public bool IsDateAvailable { get; set; }
        public DateTime ProposedDateFor { get; set; }
        public bool WasAnswered { get; set; }
    }
}
