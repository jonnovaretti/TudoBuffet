using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/area-logada/buffet")]
    [Authorize]
    [ApiController]
    public class BuffetAccountController : LoggedControllerBase
    {
        IBuffets buffets;

        public BuffetAccountController(IBuffets buffets)
        {
            this.buffets = buffets;
        }

        public ActionResult Post(NewBuffetModel newBuffetModel)
        {
            Buffet buffet;
            try
            {

                newBuffetModel.Validate();
                buffet = newBuffetModel.ToEntity(UserId);

                buffets.Save(buffet);

                return Ok();
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
                var purchasedPlan = new PurchasedPlanModel()
                {
                    Name = buffet.Name,
                    ActivedAt = buffet.ActivedAt.Value.ToString("dd/MM/yyyy"),
                    Id = buffet.Id.ToString().Substring(0, 6),
                    NamePlan = buffet.PlanSelected.Name,
                    Status = buffet.PlanSelected.IsActive ? "Ativo" : "Inativo"
                };

                purchasedPlans.Add(purchasedPlan);
            }

            return Ok(purchasedPlans);
        }
    }
}