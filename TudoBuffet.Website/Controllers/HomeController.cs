using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBuffets buffets;

        public HomeController(IBuffets buffets)
        {
            this.buffets = buffets;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel;
            List<Buffet> buffetsFound;

            buffetsFound = buffets.GetBuffetsHighlighWeek();

            homeViewModel = new HomeViewModel();

            foreach (var buffetFound in buffetsFound)
            {
                string thumbnailUrl;

                if (buffetFound.Photos.Any())
                {
                    thumbnailUrl = buffetFound.Photos.First().ThumbnailUrl;

                    var buffetHighlightWeek = new BuffetHighlightWeekModel()
                    {
                        Category = Enum.GetName(typeof(BuffetCategory), buffetFound.Category),
                        Id = buffetFound.Id,
                        Name = buffetFound.Name,
                        ThumbnailUrl = thumbnailUrl
                    };

                    homeViewModel.WeekHighlightBuffets.Add(buffetHighlightWeek);
                }
            }

            return View(homeViewModel);
        }
    }
}