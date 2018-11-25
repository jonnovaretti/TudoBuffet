using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IBuffets
    {
        IEnumerable<Buffet> GetBuffetsFromUserId(Guid userId);
        Guid Save(Buffet buffet);
        Buffet GetBuffetsById(string buffetId);
    }
}
