using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class SearchBuffetsViewModel
    {
        public SearchBuffetsViewModel()
        {
            BuffetsSearchFound = new List<BuffetSearchModel>();
        }

        public List<BuffetSearchModel> BuffetsSearchFound { get; set; }
    }
}
