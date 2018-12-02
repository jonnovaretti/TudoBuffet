namespace TudoBuffet.Website.Entities
{
    public class Photo : BaseEntity
    {
        public Buffet Buffet { get; set; }
        public string Url { get; set; }
        public string ContainerName { get; set; }
        public string FileName { get; set; }
        public string ThumbnailName { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsMainPhoto { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
    }
}
