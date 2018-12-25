using System.Collections.Generic;
using TudoBuffet.Website.Models.Bases;

namespace TudoBuffet.Website.Models
{
    public class SearchBuffetsViewModel
    {
        public SearchBuffetsViewModel()
        {
            BuffetsSearchFound = new List<BuffetSearchModel>();
        }

        public List<BuffetSearchModel> BuffetsSearchFound { get; set; }
        public IEnumerable<BuffetCategoryModel> Categories { get; set; }
        public IEnumerable<RangePriceModel> RangesPrices { get; set; }
        public IEnumerable<EnvironmentModel> Environments { get; set; }

        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
