using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IBuffets
    {
        IEnumerable<Buffet> GetBuffetsFromUserId(Guid userId);
        Guid Save(Buffet buffet);
        Buffet GetBuffetsById(Guid buffetId);
        List<Buffet> GetBuffetsHighlighWeek();
        IEnumerable<Buffet> GetBuffets(string uf, string cidade, BuffetCategory? buffetCategory, BuffetEnvironment? buffetEnvironment, RangePrice? rangePrice, string name = null);
        IEnumerable<Buffet> GetBuffetsByIds(List<string> list);
    }
}
