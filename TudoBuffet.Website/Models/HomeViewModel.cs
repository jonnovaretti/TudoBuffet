using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            WeekHighlightBuffets = new List<BuffetHighlightWeekModel>();
        }

        public List<BuffetHighlightWeekModel> WeekHighlightBuffets { get; set; }
    }
}
