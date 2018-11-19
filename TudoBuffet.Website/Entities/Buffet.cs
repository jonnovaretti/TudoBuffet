using System;

namespace TudoBuffet.Website.Entities
{
    public class Buffet : BaseEntity
    {
        public CategoryBuffet Category { get; set; }
        public string Name { get; set; }
        public Guid Id { get; set; }
        public string Thumbprint { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string CelPhone { get; set; }
    }

    public enum CategoryBuffet
    {
        Casamento,
        Infantil,
        Evento
    }
}
