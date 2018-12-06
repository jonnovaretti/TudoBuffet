namespace TudoBuffet.Website.Entities
{
    public class Photo : BaseEntity
    {
        public Buffet Buffet { get; set; }
        public string ContainerName { get; set; }
        public string DetailFileName { get; set; }
        public string DetailUrl { get; set; }
        public string SearchFileName { get; set; }
        public string SearachUrl { get; set; }
        public string ThumbnailFileName { get; set; }
        public string ThumbnailUrl { get; set; }
        public long Size { get; set; }
        public string Type { get; set; }
    }
}
