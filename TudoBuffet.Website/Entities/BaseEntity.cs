using System;

namespace TudoBuffet.Website.Entities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CreateAt = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
