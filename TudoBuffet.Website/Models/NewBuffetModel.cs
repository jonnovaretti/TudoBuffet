using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class NewBuffetModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Cellphone { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public Guid SelectedPlan { get; set; }
        public string Category { get; set; }
        public string RangePrice { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Nome é obrigatório");

            if (string.IsNullOrEmpty(Description))
                throw new ArgumentNullException("Apresentação é obrigatório");

            if (string.IsNullOrEmpty(Street))
                throw new ArgumentNullException("Rua/Av é obrigatório");

            if (string.IsNullOrEmpty(District))
                throw new ArgumentNullException("Bairro é obrigatório");

            if (string.IsNullOrEmpty(City))
                throw new ArgumentNullException("Cidade é obrigatório");

            if (string.IsNullOrEmpty(State))
                throw new ArgumentNullException("Cidade é obrigatório");
        }

        public Buffet ToEntity(Guid ownerId)
        {
            Buffet buffet;

            buffet = new Buffet()
            {
                Category = (BuffetCategory)Enum.Parse(typeof(BuffetCategory), Category),
                Cellphone = Cellphone,
                City = City,
                Description = Description,
                District = District,
                Facebook = Facebook,
                Instagram = Instagram,
                Number = Number,
                Owner = new User() { Id = ownerId },
                Name = Name,
                PlanSelected = new Plan() { Id = SelectedPlan },
                RangePrice = (PricesOptions)Enum.Parse(typeof(PricesOptions), RangePrice),
                State = State,
                Street = Street
            };

            return buffet;
        }
    }
}
