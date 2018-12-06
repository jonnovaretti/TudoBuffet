using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("buffet")]
    public class BuffetController : Controller
    {
        private readonly IBuffets buffets;

        public BuffetController(IBuffets buffets)
        {
            this.buffets = buffets;
        }

        [HttpGet]
        [Route("busca")]
        public IActionResult SearchBuffets(FilterBuffetSearch filters)
        {
            SearchBuffetsViewModel searchBuffetsViewModel;
            IEnumerable<Buffet> buffetsFound;
            BuffetCategory? buffetCategory;
            BuffetEnvironment? buffetEnvironment;
            RangePrice? rangePrice;

            buffetCategory = string.IsNullOrEmpty(filters.Category) ? null : (BuffetCategory?) Enum.Parse(typeof(BuffetCategory), filters.Category);
            buffetEnvironment = string.IsNullOrEmpty(filters.Environment) ? null : (BuffetEnvironment?)Enum.Parse(typeof(BuffetEnvironment), filters.Environment);
            rangePrice = string.IsNullOrEmpty(filters.RangePrice) ? null : (RangePrice?)Enum.Parse(typeof(RangePrice), filters.RangePrice);

            buffetsFound = buffets.GetBuffets(filters.State, filters.City, buffetCategory, buffetEnvironment, rangePrice, filters.Name);

            searchBuffetsViewModel = new SearchBuffetsViewModel();

            foreach (var buffetFound in buffetsFound)
            {
                var buffetFoundModel = new BuffetSearchModel()
                {
                    City = buffetFound.City,
                    Id = buffetFound.Id,
                    Name = buffetFound.Name,
                    State = buffetFound.State,
                    FirstThumbnailUrl = buffetFound.Photos.Any() ? buffetFound.Photos.First().DetailUrl : string.Empty,
                    SecondThumbnailUrl = buffetFound.Photos.Any() ? buffetFound.Photos.Last().DetailUrl : string.Empty
                };

                searchBuffetsViewModel.BuffetsSearchFound.Add(buffetFoundModel);
            }

            return View(searchBuffetsViewModel);
        }

        [HttpGet]
        [Route("detalhe/{buffetId}")]
        public IActionResult GetBuffetDetail(string buffetId)
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
                ThumbnailsUrls = buffetFound.Photos.Select(p => p.ThumbnailUrl).ToList()
            };

            detailViewModel = new DetailViewModel();
            detailViewModel.BuffetDetail = buffetDetail;

            return View(detailViewModel);
        }
    }
}