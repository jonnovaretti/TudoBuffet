using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;

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
                Environment = EnvironmentModel.CreateEnvironmentModel(Enum.GetName(typeof(BuffetEnvironment), buffet.Environment)).Text,
                Id = buffet.Id,
                Name = buffet.Name,
                RangePrice = RangePriceModel.CreateRangePriceModel(Enum.GetName(typeof(RangePrice), buffet.Price)).Text,
                ThumbnailUrl = buffet.Photos.First().ThumbnailUrl
            });
        }

        public List<BuffetBudgetSelectedModel> BuffetsBudgetSelected { get; }
        public BudgetSentModel BudgetSent { get; set; }
    }
}
