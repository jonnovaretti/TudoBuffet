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
    [Route("api/admin/buffet")]
    [Authorize]
    [ApiController]
    public class BuffetAccountController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IBlobFileHandler photoHandlerService;

        public BuffetAccountController(IBuffets buffets, IBlobFileHandler photoHandlerService)
        {
            this.buffets = buffets;
            this.photoHandlerService = photoHandlerService;
        }

        public ActionResult Post(NewBuffetModel newBuffetModel)
        {
            Buffet buffet;
            Guid buffetId;

            try
            {
                newBuffetModel.Validate();
                buffet = newBuffetModel.ToEntity(UserId);

                buffetId = buffets.Save(buffet);

                return Ok(buffetId);
            }
            catch (Exception ex)    
            {
                return ServerError();
            }
        }
       
        [Route("planos-contratados")]
        [HttpGet]
        public ActionResult<List<PurchasedPlanModel>> GetPurchasedPlans()
        {
            List<PurchasedPlanModel> purchasedPlans;
            IEnumerable<Buffet> buffetsFound;
            
            buffetsFound = buffets.GetBuffetsFromUserId(UserId);

            purchasedPlans = new List<PurchasedPlanModel>();

            foreach (var buffet in buffetsFound)
            {
                if (buffet != null)
                {
                    var purchasedPlan = new PurchasedPlanModel()
                    {
                        Name = buffet.Name,
                        ActivedAt = buffet.ActivedAt.HasValue ? buffet.ActivedAt.Value.ToString("dd/MM/yyyy") : string.Empty,
                        Id = buffet.Id.ToString().Substring(0, 6),
                        NamePlan = buffet.PlanSelected.Name,
                        Status = buffet.PlanSelected.IsActive ? "Ativo" : "Inativo"
                    };

                    purchasedPlans.Add(purchasedPlan);
                }
            }

            return Ok(purchasedPlans);
        }
    }
}