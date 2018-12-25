using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Infrastructures.Contracts;
using TudoBuffet.Website.Infrastructures;
using System.Threading.Tasks;
using TudoBuffet.Website.Repositories.Paging;
using TudoBuffet.Website.Models.Bases;

namespace TudoBuffet.Website.Controllers
{
    [Route("buffets")]
    public class BuffetsController : Controller
    {
        private readonly IBuffets buffets;
        private readonly IHttpContextAccessor httpContext;
        private readonly IIpLocalizator ipLocalizator;

        public BuffetsController(IBuffets buffets, IHttpContextAccessor httpContext, IIpLocalizator ipLocalizator)
        {
            this.buffets = buffets;
            this.httpContext = httpContext;
            this.ipLocalizator = ipLocalizator;
        }

        [HttpGet]
        public async Task<IActionResult> Index(FilterBuffetSearch filters)
        {
            SearchBuffetsViewModel searchBuffetsViewModel;
            BuffetCategory? buffetCategory;
            BuffetEnvironment? buffetEnvironment;
            PagedQuery<Buffet> pagedQuery;
            RangePrice? rangePrice;
            GeoLocation geoLocation;

            buffetCategory = string.IsNullOrEmpty(filters.Category) ? null : (BuffetCategory?)Enum.Parse(typeof(BuffetCategory), filters.Category);
            buffetEnvironment = string.IsNullOrEmpty(filters.Environment) ? null : (BuffetEnvironment?)Enum.Parse(typeof(BuffetEnvironment), filters.Environment);
            rangePrice = string.IsNullOrEmpty(filters.RangePrice) ? null : (RangePrice?)Enum.Parse(typeof(RangePrice), filters.RangePrice);

            if (string.IsNullOrEmpty(filters.State) && string.IsNullOrEmpty(filters.City))
            {
                geoLocation = await ipLocalizator.GetCountryFromIp(httpContext.HttpContext.Connection.RemoteIpAddress.ToString());

                filters.State = geoLocation.State;
                filters.City = geoLocation.City;
            }

            pagedQuery = await buffets.GetBuffets(filters.Page, filters.PageSize, filters.State, filters.City, buffetCategory, buffetEnvironment, rangePrice, filters.Name);

            searchBuffetsViewModel = new SearchBuffetsViewModel();
            searchBuffetsViewModel.PageSize = pagedQuery.PageSize;
            searchBuffetsViewModel.CurrentPage = pagedQuery.CurrentPage;
            searchBuffetsViewModel.TotalItems = pagedQuery.TotalItems;

            foreach (var buffetFound in pagedQuery.Result)
            {
                var buffetFoundModel = new BuffetSearchModel()
                {
                    City = buffetFound.City,
                    Id = buffetFound.Id,
                    Name = buffetFound.Name,
                    State = buffetFound.State,
                    FirstThumbnailUrl = buffetFound.Photos.Any() ? buffetFound.Photos.First().SearchUrl : string.Empty,
                    SecondThumbnailUrl = buffetFound.Photos.Any() ? buffetFound.Photos.Last().SearchUrl : string.Empty,
                    Category = buffetFound.Category.ToString()
                };

                searchBuffetsViewModel.BuffetsSearchFound.Add(buffetFoundModel);
            }

            searchBuffetsViewModel.Categories = BuffetCategoryModel.GetBuffetCategories();
            searchBuffetsViewModel.Environments = EnvironmentModel.GetEnvironments();
            searchBuffetsViewModel.RangesPrices = RangePriceModel.GetRangePriceList();

            searchBuffetsViewModel.Categories.ToList().ForEach(c => FillQueryString(c, "categoria"));
            searchBuffetsViewModel.Environments.ToList().ForEach(e => FillQueryString(e, "ambiente"));
            searchBuffetsViewModel.RangesPrices.ToList().ForEach(r => FillQueryString(r, "faixadepreco"));

            return View(searchBuffetsViewModel);
        }

        [HttpGet]
        [Route("detalhe/{buffetId}")]
        public IActionResult Detail(string buffetId)
        {
            Buffet buffetFound;
            BuffetDetailModel buffetDetail;
            DetailViewModel detailViewModel;
            RangePriceModel rangePriceModel;
            EnvironmentModel environmentModel;

            buffetFound = buffets.GetBuffetsById(Guid.Parse(buffetId));

            rangePriceModel = RangePriceModel.CreateRangePriceModel(Enum.GetName(typeof(RangePrice), buffetFound.Price));
            environmentModel = EnvironmentModel.CreateEnvironmentModel(Enum.GetName(typeof(BuffetEnvironment), buffetFound.Environment));

            buffetDetail = new BuffetDetailModel()
            {
                Name = buffetFound.Name,
                Category = Enum.GetName(typeof(BuffetCategory), buffetFound.Category),
                Description = buffetFound.Description,
                Location = string.Concat(buffetFound.Street, ", ", buffetFound.Number, ", ", buffetFound.District, " - ", buffetFound.City, "-", buffetFound.State),
                RangePrince = rangePriceModel.Text,
                EnvironmentType = environmentModel.Text,
                PhotosUrls = buffetFound.Photos.Select(p => p.DetailUrl).ToList(),
                ThumbnailsUrls = buffetFound.Photos.Select(p => p.ThumbnailUrl).ToList(),
                Id = buffetFound.Id
            };

            detailViewModel = new DetailViewModel();
            detailViewModel.BuffetDetail = buffetDetail;

            return View(detailViewModel);
        }
    }
}