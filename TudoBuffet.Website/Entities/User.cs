using System;
using System.ComponentModel.DataAnnotations;
using TudoBuffet.Website.Exceptions;
using TudoBuffet.Website.ValuesObjects;

namespace TudoBuffet.Website.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public DateTime? ActivedAt { get; set; }
        public Profile Profile { get; set; }
        public string Discriminator
        {
            get { return Enum.GetName(typeof(Profile), Profile); }
            set { Profile = (Profile) Enum.Parse(typeof(Profile), value); }
        }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
                throw new BusinessException("Campo obrigatório, e-mail deve ser preenchido");

            if (!new EmailAddressAttribute().IsValid(Email))
                throw new BusinessException("E-mail inválido");

            if (string.IsNullOrEmpty(Name))
                throw new BusinessException("Campo obrigatório, nome deve ser preenchido");

            if (string.IsNullOrEmpty(PasswordHash))
                throw new BusinessException("Campo obrigatório, senha deve ser preenchido");
        }

        public void Active()
        {
            IsActive = true;
            ActivedAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetNewSignup()
        {
            IsActive = false;
            CreateAt = DateTime.Now;
        }
    }
}