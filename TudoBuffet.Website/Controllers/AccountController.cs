using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using System.Security.Claims;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/conta-logada")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IBuffets buffets;

        public AccountController(IBuffets buffets)
        {
            this.buffets = buffets;
        }

        [Route("planos-contratados")]
        [HttpGet]
        public ActionResult<List<PurchasedPlan>> GetPurchasedPlans()
        {
            List<PurchasedPlan> purchasedPlans;
            IEnumerable<Buffet> buffetsFound;
            Guid idParsed;
            Claim claim;

            claim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == "id");
            idParsed = Guid.Parse(claim.Value);

            buffetsFound = buffets.GetBuffetsFromUserId(idParsed);

            purchasedPlans = new List<PurchasedPlan>();

            foreach (var buffet in buffetsFound)
            {
                var purchasedPlan = new PurchasedPlan()
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