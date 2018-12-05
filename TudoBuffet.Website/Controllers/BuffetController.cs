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

        [Route("detalhe/{buffetId}")]
        [HttpGet]
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
                PhotosUrls = buffetFound.Photos.Select(p => p.Url).ToList(),
                ThumbnailsUrls = buffetFound.Photos.Select(p => p.ThumbnailUrl).ToList()
            };

            detailViewModel = new DetailViewModel();
            detailViewModel.BuffetDetail = buffetDetail;

            return View(detailViewModel);
        }
    }
}