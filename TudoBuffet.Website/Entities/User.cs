using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PasswordHash { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActivedAt { get; set; }
        public List<Buffet> Buffets { get; set; }

        public void Active()
        {
            IsActive = true;
            ActivedAt = DateTime.UtcNow;
        }
    }
}