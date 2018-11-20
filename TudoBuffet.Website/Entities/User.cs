using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TudoBuffet.Website.Exceptions;

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
        public List<Buffet> Buffets { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Email))
                throw new BusinessException("Campo obrigatório, e-mail não preenchido");

            if (!new EmailAddressAttribute().IsValid(Email))
                throw new BusinessException("E-mail inválido");

            if (string.IsNullOrEmpty(Name))
                throw new BusinessException("Campo obrigatório, nome não preenchido");

            if (string.IsNullOrEmpty(PasswordHash))
                throw new BusinessException("Campo obrigatório, senha não preenchido");
        }

        public void Active()
        {
            IsActive = true;
            ActivedAt = DateTime.UtcNow;
            UpdateAt = DateTime.UtcNow;
        }
    }
}