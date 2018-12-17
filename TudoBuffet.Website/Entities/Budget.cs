using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class Budget : BaseEntity
    {
        public UserCustomer Customer { get; set; }
        public List<BudgetBuffet> BudgetBuffets { get; set; }
        public string EmailSender { get; set; }
        public int QuantityPartyGuests { get; set; }
        public DateTime DayParty { get; set; }
        public string Observation { get; set; }
    }
}
