using System.Collections.Generic;

namespace TudoBuffet.Website.Entities
{
    public class UserBuffetAdmin : User
    {
        public List<Buffet> Buffets { get; set; }
    }
}
