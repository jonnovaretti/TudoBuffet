using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("admin/buffet")]
    [Authorize(Roles = "BuffetAdmin")]
    public class BuffetAdminController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotoHandler photoHandlerService;
        private readonly IPlans plans;

        public BuffetAdminController(IBuffets buffets, IPhotoHandler photoHandlerService, IPlans plans)
        {
            this.buffets = buffets;
            this.photoHandlerService = photoHandlerService;
            this.plans = plans;
        }

        [Route("")]
        [HttpGet]
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
                        ActivedAt = buffet.ActivedAt.HasValue ? buffet.ActivedAt.Value.ToString("dd/MM/yyyy") : string.Empty,
                        Id = buffet.Id.ToString().Substring(0, 6),
                        NamePlan = buffet.PlanSelected.Name,
                        Status = buffet.PlanSelected.IsActive ? "Ativo" : "Inativo"
                    };

                    purchasedBuffetAds.Add(purchasedPlan);
                }
            }

            buffetAdmin = new BuffetAdminViewModel();
            buffetAdmin.PurchasedBuffetAds = purchasedBuffetAds;
            buffetAdmin.OwnerName = FullName;

            return View(buffetAdmin);
        }

        [HttpGet]
        [Route("novo")]
        public ActionResult NewBuffet()
        {
            IEnumerable<Plan> plansFound;
            NewBuffetViewModel newBuffetViewModel;

            plansFound = plans.GetAll();

            newBuffetViewModel = new NewBuffetViewModel();
            newBuffetViewModel.MapperPlans(plansFound);

            return View(newBuffetViewModel);
        }

        [HttpPost]
        [Route("novo")]
        public ActionResult NewBuffet(NewBuffetViewModel newBuffetModel)
        {
            Buffet buffet;
            Guid buffetId;

            try
            {
                newBuffetModel.Buffet.Validate();
                buffet = newBuffetModel.Buffet.ToEntity(UserId);

                buffetId = buffets.Save(buffet);

                return Ok(buffetId);
            }
            catch (Exception ex)    
            {
                return ServerError();
            }
        }
    }
}