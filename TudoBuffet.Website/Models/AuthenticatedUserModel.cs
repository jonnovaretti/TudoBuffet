using System;

namespace TudoBuffet.Website.Models
{
    public class AuthenticatedUserModel
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
    }
}
