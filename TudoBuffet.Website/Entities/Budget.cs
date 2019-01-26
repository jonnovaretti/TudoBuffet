using System;
using System.Collections.Generic;
using System.Linq;

namespace TudoBuffet.Website.Entities
{
    public class Budget : BaseEntity
    {
        public Budget()
        {
            Details = new List<BudgetDetail>();
        }

        public UserPartyOwner PartyOwner { get; set; }
        public int QuantityPartyGuests { get; set; }
        public DateTime PartyDay { get; set; }
        public List<BudgetDetail> Details { get; set; }

        public bool BelongsToBuffetAdmin(Guid userId)
        {
            return Details.First().Buffet.Owner.Id == userId;
        }
    }
}
