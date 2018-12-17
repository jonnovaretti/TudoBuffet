using System;
using System.ComponentModel.DataAnnotations;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class BuffetModel
    {
        [Required(ErrorMessage ="Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Apresentação é obrigatório")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Cep é obrigatório")]
        public string Zipcode { get; set; }
        [Required(ErrorMessage = "Rua/Av é obrigatório")]
        public string Street { get; set; }
        public string Number { get; set; }
        [Required(ErrorMessage = "Bairro é obrigatório")]
        public string District { get; set; }
        [Required(ErrorMessage = "Cidade é obrigatório")]
        public string City { get; set; }
        [Required(ErrorMessage = "UF é obrigatório")]
        public string State { get; set; }
        public string Cellphone { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        [Required(ErrorMessage = "Selecione um plano")]
        public Guid SelectedPlan { get; set; }
        [Required(ErrorMessage = "Faixa de preço é obrigatório")]
        public string SelectedRangePrice { get; set; }
        [Required(ErrorMessage = "Categoria é obrigatório")]
        public string SelectedBuffetCategory { get; set; }
        [Required(ErrorMessage = "Ambiente é obrigatório")]
        public string SelectedBuffetEnvironment { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                throw new ArgumentNullException("Nome é obrigatório");

            if (string.IsNullOrEmpty(Description))
                throw new ArgumentNullException("Apresentação é obrigatório");

            if (string.IsNullOrEmpty(Zipcode))
                throw new ArgumentNullException("Cep é obrigatório");

            if (string.IsNullOrEmpty(Street))
                throw new ArgumentNullException("Rua/Av é obrigatório");

            if (string.IsNullOrEmpty(District))
                throw new ArgumentNullException("Bairro é obrigatório");

            if (string.IsNullOrEmpty(City))
                throw new ArgumentNullException("Cidade é obrigatório");

            if (string.IsNullOrEmpty(State))
                throw new ArgumentNullException("Cidade é obrigatório");

            if (string.IsNullOrEmpty(SelectedRangePrice))
                throw new ArgumentNullException("Faixa de preço é obrigatório");

            if (string.IsNullOrEmpty(SelectedBuffetCategory))
                throw new ArgumentNullException("Categoria é obrigatório");

            if (string.IsNullOrEmpty(SelectedBuffetEnvironment))
                throw new ArgumentNullException("Ambiente é obrigatório");
        }

        public Buffet ToEntity(Guid ownerId)
        {
            Buffet buffet;

            buffet = new Buffet()
            {
                Category = (BuffetCategory)Enum.Parse(typeof(BuffetCategory), SelectedBuffetCategory),
                Cellphone = Cellphone,
                City = City,
                Description = Description,
                District = District,
                Facebook = Facebook,
                Instagram = Instagram,
                Number = Number,
                Owner = new UserBuffetAdmin() { Id = ownerId },
                Name = Name,
                PlanSelected = new Plan() { Id = SelectedPlan },
                Price = (RangePrice)Enum.Parse(typeof(RangePrice), SelectedRangePrice),
                State = State,
                Street = Street,
                Environment = (BuffetEnvironment)Enum.Parse(typeof(BuffetEnvironment), SelectedBuffetEnvironment )
            };

            return buffet;
        }
    }
}
