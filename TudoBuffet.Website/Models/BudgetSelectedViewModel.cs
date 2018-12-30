using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Models
{
    public class BudgetSelectedViewModel
    {
        public BudgetSelectedViewModel()
        {
            BuffetsBudgetSelected = new List<BuffetBudgetSelectedModel>();
            BudgetSent = new BudgetSentModel();
        }

        public void AddBuffet(Buffet buffet)
        {
            BuffetsBudgetSelected.Add(new BuffetBudgetSelectedModel()
            {
                Category = Enum.GetName(typeof(BuffetCategory), buffet.Category),
                Environment = EnvironmentModel.Create(buffet.Environment).Text,
                Id = buffet.Id,
                Name = buffet.Name,
                RangePrice = RangePriceModel.Create(buffet.Price).Text,
                ThumbnailUrl = buffet.Photos.First().ThumbnailUrl
            });
        }

        public List<BuffetBudgetSelectedModel> BuffetsBudgetSelected { get; }
        public BudgetSentModel BudgetSent { get; set; }
    }
}
