using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetViewModel
    {
        public BuffetModel Buffet { get; set; }
        public List<RangePriceModel> RangesPrices { get; set; }
        public List<EnvironmentModel> Environments { get; set; }
        public List<BuffetCategoryModel> Categories { get; set; }
        public List<PlanModel> Plans { get; set; }

        public BuffetViewModel()
        {
            RangesPrices = RangePriceModel.GetRangePriceList();
            Environments = EnvironmentModel.GetEnvironments();
            Categories = BuffetCategoryModel.GetBuffetCategories();
        }

        public void MapperPlans(IEnumerable<Plan> plansFound)
        {
            Plans = new List<PlanModel>();

            foreach (var plan in plansFound)
            {
                Plans.Add(new PlanModel {
                    Id = plan.Id,
                    Name = plan.Name,
                    Image = plan.Image,
                    Description = plan.Description
                });
            }
        }
    }
}
