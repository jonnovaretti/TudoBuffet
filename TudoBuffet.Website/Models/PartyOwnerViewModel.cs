using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class PartyOwnerViewModel
    {
        public PartyOwnerViewModel()
        {
            Budgets = new List<BudgetModel>();
        }

        public List<BudgetModel> Budgets { get; set; }
        public string OwnerName { get; set; }

        public void AddBudget(IEnumerable<Budget> budgets)
        {
            foreach (var budget in budgets)
            {
                foreach (var detail in budget.Details)
                {
                    Budgets.Add(new BudgetModel()
                    {
                        PartyDay = budget.PartyDay,
                        QuantityGuests = budget.QuantityPartyGuests,
                        Detail = CreateDetail(detail)
                    });
                }
            }
        }

        private BudgetDetailModel CreateDetail(BudgetDetail detail)
        {
            return new BudgetDetailModel()
            {
                BuffetName = detail.Buffet.Name,
                IsDateAvailable = detail.IsDateAvaliable.HasValue ? detail.IsDateAvaliable.Value : true,
                DetailId = detail.Id,
                Title = detail.Buffet.Title,
                WasAnswered = detail.AnsweredAt.HasValue
            };
        }
    }
}
