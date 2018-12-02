using System;

namespace TudoBuffet.Website.Models
{
    public class PhotoUploadedModel
    {
        public string ThumbnailUrl { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string DeleteType { get; set; }
        public string Type { get; set; }
        public string DeleteUrl { get; set; }
        public long Size { get; set; }
        public Guid Id { get; set; }
    }
}
