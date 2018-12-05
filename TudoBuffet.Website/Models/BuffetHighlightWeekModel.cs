using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetHighlightWeekModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string ThumbnailUrl { get; set; }
    }
}
