using System;

namespace TudoBuffet.Website.Models
{
    public class PlanModel
    {
        public Guid Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}