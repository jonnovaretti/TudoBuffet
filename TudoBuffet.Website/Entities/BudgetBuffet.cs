using System;

namespace TudoBuffet.Website.Entities
{
    public class BudgetBuffet
    {
        public Guid BudgetId { get; set; }
        public Budget Budget { get; set; }
        public Guid BuffetId { get; set; }
        public Buffet Buffet { get; set; }
    }
}
