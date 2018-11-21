using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetTopModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public BuffetCategory Category { get; set; }
        public string Thumbprint { get; set; }
    }
}
