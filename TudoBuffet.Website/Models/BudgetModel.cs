using System;

namespace TudoBuffet.Website.Models
{
    public class BudgetModel
    {
        public BudgetModel()
        {
            Detail = new BudgetDetailModel();
        }

        public int QuantityGuests { get; set; }
        public DateTime PartyDay { get; set; }
        public BudgetDetailModel Detail { get; set; }
    }
}