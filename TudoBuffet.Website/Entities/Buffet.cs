using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class Buffet : BaseEntity
    {
        public User Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public int? Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cellphone { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public List<Photo> Photos { get; set; }
        public Plan PlanSelected { get; set; }
        public BuffetCategory Category { get; set; }
        public RangePrice Price { get; set; }
        public DateTime? ActivedAt { get; set; }
        public BuffetEnvironment Environment { get; set; }
    }

    public enum RangePrice
    {
        Less2000,
        Between2000And4000,
        Between4000And6000,
        Between6000And8000,
        Between8000And12000,
        More12000
    }

    public enum BuffetCategory
    {
        Casamento,
        Infantil,
        Evento
    }

    public enum BuffetEnvironment
    {
        SalaoDeFesta,
        Fazenda,
        Clube,
        Restaurante,
        AreaDeEntretenimento,
        Praia,
        SitioChacara
    }
}
