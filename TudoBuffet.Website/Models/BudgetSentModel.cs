using System;

namespace TudoBuffet.Website.Models
{
    public class BudgetSentModel
    {
        public string EmailSender { get; set; }
        public int QuantityPartyGuests { get; set; }
        public DateTime DayParty { get; set; }
        public string Observation { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(EmailSender))
                throw new ArgumentException("Email é campo obrigatório");

            if (QuantityPartyGuests == 0)
                throw new ArgumentException("Quantidade de convidados é obrigatório");

            if (DayParty.Date < DateTime.Now.Date)
                throw new ArgumentException("Data da festa é obrigatório e deve ser maior que a data de hoje");
        }
    }
}
