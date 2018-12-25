using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models.Bases;
using TudoBuffet.Website.Repositories.Paging;

namespace TudoBuffet.Website.Models
{
    public class SearchBuffetsViewModel
    {
        public SearchBuffetsViewModel()
        {
            BuffetsSearchFound = new List<BuffetSearchModel>();
        }

        public List<BuffetSearchModel> BuffetsSearchFound { get; set; }
        public IEnumerable<BuffetCategoryModel> Categories { get; set; }
        public IEnumerable<RangePriceModel> RangesPrices { get; set; }
        public IEnumerable<EnvironmentModel> Environments { get; set; }
        public IEnumerable<QueryStringModelBase> Pages { get; set; }

        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        public List<QueryStringModelBase> GeneratePages(int pageCount)
        {
            List<QueryStringModelBase> pages;

            pages = new List<QueryStringModelBase>();

            for (int i = 1; i <= pageCount; i++)
            {
                var page = new QueryStringModelBase() { Code = i.ToString() };

                pages.Add(page);
            }

            return pages;
        }

        public static SearchBuffetsViewModel Create(PagedQuery<Buffet> pagedQuery, string queryString)
        {
            SearchBuffetsViewModel searchBuffetsViewModel = new SearchBuffetsViewModel();

            searchBuffetsViewModel.PageSize = pagedQuery.PageSize;
            searchBuffetsViewModel.CurrentPage = pagedQuery.CurrentPage;
            searchBuffetsViewModel.TotalItems = pagedQuery.TotalItems;
            searchBuffetsViewModel.TotalPages = (int)Math.Ceiling((decimal)(pagedQuery.TotalItems / pagedQuery.PageSize)) + 1;
            searchBuffetsViewModel.Pages = searchBuffetsViewModel.GeneratePages(searchBuffetsViewModel.TotalPages);

            foreach (var buffetFound in pagedQuery.Result)
            {
                var buffetFoundModel = BuffetSearchModel.ToModel(buffetFound);

                searchBuffetsViewModel.BuffetsSearchFound.Add(buffetFoundModel);
            }

            searchBuffetsViewModel.Categories = BuffetCategoryModel.GetBuffetCategories();
            searchBuffetsViewModel.Environments = EnvironmentModel.GetEnvironments();
            searchBuffetsViewModel.RangesPrices = RangePriceModel.GetRangePriceList();

            searchBuffetsViewModel.Categories.ToList().ForEach(c => c.FormatQueryString("categoria", queryString));
            searchBuffetsViewModel.Environments.ToList().ForEach(e => e.FormatQueryString("ambiente", queryString));
            searchBuffetsViewModel.RangesPrices.ToList().ForEach(r => r.FormatQueryString("faixadepreco", queryString));
            searchBuffetsViewModel.Pages.ToList().ForEach(p => p.FormatQueryString("pagina", queryString));

            return searchBuffetsViewModel;
        }
    }
}
