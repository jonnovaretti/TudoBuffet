using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class BudgetSentModel
    {
        public Guid BuffetId { get; set; }
        public Guid PartyOwnerId { get; set; }

        public int QuantityPartyGuests { get; set; }
        public DateTime PartyDay { get; set; }
        public List<string> Questions { get; set; }

        public void Validate()
        {
            if (QuantityPartyGuests == 0)
                throw new ArgumentException("Quantidade de convidados é obrigatório");

            if (PartyDay.Date < DateTime.Now.Date)
                throw new ArgumentException("Data da festa é obrigatório e deve ser maior que a data de hoje");
        }
    }
}
