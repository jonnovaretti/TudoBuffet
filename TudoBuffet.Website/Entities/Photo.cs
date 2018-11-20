namespace TudoBuffet.Website.Entities
{
    public class Photo : BaseEntity
    {
        public Buffet Buffet { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }
        public bool IsMainPhoto { get; set; }
    }
}
