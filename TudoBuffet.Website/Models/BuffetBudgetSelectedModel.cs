using System;

namespace TudoBuffet.Website.Models
{
    public class BuffetBudgetSelectedModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string RangePrice { get; set; }
        public string Environment { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
