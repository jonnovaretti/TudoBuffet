using System;

namespace TudoBuffet.Website.Models
{
    public class AuthenticatedUser
    {
        public string Token { get; set; }
        public Guid Id { get; set; }
    }
}
