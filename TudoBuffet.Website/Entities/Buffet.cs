using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class Buffet : BaseEntity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Facebook { get; set; }
        public string CelPhone { get; set; }
        public List<Photo> Photos { get; set; }
        public Plan PlanSelected { get; set; }
        public BuffetCategory Category { get; set; }
        public PricesOptions PriceRange { get; set; }
        public DateTime? ActivedAt { get; set; }
        public DateTime? ActiveUntil { get; set; }
    }

    public enum PricesOptions
    {
        less1000,
        between1000And3000,
        between3000And5000,
        between5000And8000,
        between8000And12000,
        more12000
    }

    public enum BuffetCategory
    {
        Casamento,
        Infantil,
        Evento
    }
}
