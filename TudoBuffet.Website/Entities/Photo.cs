namespace TudoBuffet.Website.Entities
{
    public class Photo : BaseEntity
    {
        public Buffet Buffet { get; set; }
        public string Url { get; set; }
        public string UrlThumbnail { get; set; }
        public bool IsMainPhoto { get; set; }
    }
}
