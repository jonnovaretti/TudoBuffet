using System;

namespace TudoBuffet.Website.Models
{
    public class ReceivedBudgetModel
    {
        public Guid BudgetDetailId { get; set; }
        public string OwnerPartyName { get; set; }
        public DateTime PartyDay { get; set; }
        public int QuantityPartyGuests { get; set; }
        public bool WasAnswered { get; set; }
        public DateTime SentAt { get; set; }
    }
}
