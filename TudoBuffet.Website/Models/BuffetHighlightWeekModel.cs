using System;

namespace TudoBuffet.Website.Models
{
    public class BuffetHighlightWeekModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string FirstThumbnailUrl { get; set; }
        public string SecondThumbnailUrl { get; set; }
        public string Title { get; set; }
    }
}
