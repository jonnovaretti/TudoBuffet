using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "UserBuffetAdmin")]
    public class AdminController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotoHandler photoHandlerService;
        private readonly IPlans plans;

        public AdminController(IBuffets buffets, IPhotoHandler photoHandlerService, IPlans plans)
        {
            this.buffets = buffets;
            this.photoHandlerService = photoHandlerService;
            this.plans = plans;
        }

        public IActionResult Index()
        {
            BuffetAdminViewModel buffetAdmin;
            List<PurchasedBuffetAdModel> purchasedBuffetAds;
            IEnumerable<Buffet> buffetsFound;

            buffetsFound = buffets.GetBuffetsFromUserId(UserId);

            purchasedBuffetAds = new List<PurchasedBuffetAdModel>();

            foreach (var buffet in buffetsFound)
            {
                if (buffet != null)
                {
                    var purchasedPlan = new PurchasedBuffetAdModel()
                    {
                        Name = buffet.Name,
                        ActivedAt = buffet.ActivedAt.HasValue ? buffet.ActivedAt.Value.ToString("dd/MM/yyyy") : "Aguardando ativação",
                        Id = buffet.Id.ToString().Substring(0, 6),
                        BuffetId = buffet.Id.ToString(),
                        NamePlan = buffet.PlanSelected.Name,
                        Status = buffet.ActivedAt.HasValue ? "Ativo" : "Inativo"
                    };

                    purchasedBuffetAds.Add(purchasedPlan);
                }
            }

            buffetAdmin = new BuffetAdminViewModel();
            buffetAdmin.PurchasedBuffetAds = purchasedBuffetAds;
            buffetAdmin.OwnerName = FullName;

            return View(buffetAdmin);
        }
    }
}