using System;

namespace TudoBuffet.Website.Models
{
    public class BuffetSearchModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstThumbnailUrl { get; set; }
        public string SecondThumbnailUrl { get; set; }
        public string City { get; set; }
        public string State { get; set; }
    }
}
