using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Paging;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IBuffets
    {
        IEnumerable<Buffet> GetBuffetsFromUserId(Guid userId);
        Guid Save(Buffet buffet);
        Buffet GetBuffetsById(Guid buffetId);
        List<Buffet> GetBuffetsHighlighWeek();
        Task<PagedQuery<Buffet>> GetBuffets(int page, int pageSize, string uf, string cidade, BuffetCategory? buffetCategory, BuffetEnvironment? buffetEnvironment, RangePrice? rangePrice, string name = null);
        IEnumerable<Buffet> GetBuffetsByIds(List<string> list);
        Guid Update(Guid id, Buffet buffet);
    }
}
