using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class Budget
    {
        public Guid Id { get; set; }
        public List<Buffet> BuffetsSelected { get; set; }
        public string EmailSender { get; set; }
        public int QuantityPartyGuests { get; set; }
        public DateTime DayParty { get; set; }
        public string Observation { get; set; }
    }
}
