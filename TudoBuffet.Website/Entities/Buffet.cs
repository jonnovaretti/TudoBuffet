using System;
using System.Collections.Generic;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Entities
{
    public class Buffet : BaseEntity
    {
        public UserBuffetAdmin Owner { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Zipcode { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
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
        public string Title { get; set; }
        public string UrlPage { get; set; }
    }
}
