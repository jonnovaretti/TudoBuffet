using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TudoBuffet.Website.Repositories.Paging
{
    public class PagedQuery<T>
    {
        public IEnumerable<T> Result { get; set; }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
