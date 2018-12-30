using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Infrastructures.Contracts;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Repositories.Paging;
using TudoBuffet.Website.ValuesObjects;

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

            searchBuffetsViewModel = SearchBuffetsViewModel.Create(pagedQuery, Request.QueryString.Value, filters.State, filters.City, filters.Name);

            return View(searchBuffetsViewModel);
        }
        
        [HttpGet]
        [Route("{title}")]
        public IActionResult Detail(string title)
        {
            Buffet buffetFound;
            BuffetDetailModel buffetDetail;
            DetailViewModel detailViewModel;
            RangePriceModel rangePriceModel;
            EnvironmentModel environmentModel;

            buffetFound = buffets.GetBuffetsByTitle(title);

            rangePriceModel = RangePriceModel.Create(buffetFound.Price);
            environmentModel = EnvironmentModel.Create(buffetFound.Environment);

            buffetDetail = BuffetDetailModel.Create(buffetFound, rangePriceModel, environmentModel);

            detailViewModel = new DetailViewModel();
            detailViewModel.BuffetDetail = buffetDetail;

            return View(detailViewModel);
        }

    }
}