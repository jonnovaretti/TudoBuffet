using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class BuffetDetailModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public List<string> PhotosUrls { get; set; }
        public string Location { get; set; }
        public List<string> ThumbnailsUrls { get; set; }
        public string RangePrince { get; internal set; }
        public string EnvironmentType { get; internal set; }
    }
}
