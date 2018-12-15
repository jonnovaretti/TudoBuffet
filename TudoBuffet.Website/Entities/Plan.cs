namespace TudoBuffet.Website.Entities
{
    public class Plan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; }
        public decimal Price  { get; set; }
        public string Image { get; set; }
    }
}
