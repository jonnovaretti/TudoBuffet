using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class UserOwnerBuffet : User
    {
        public List<Buffet> Buffets { get; set; }
    }
}
