using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("dono-da-festa")]
    [Authorize(Roles = "UserPartyOwner")]
    public class PartyOwnerController : AuthenticatedControllerBase
    {
        private readonly IBudgets budgets;

        public PartyOwnerController(IBudgets budgets)
        {
            this.budgets = budgets;
        }

        public IActionResult Index()
        {
            PartyOwnerViewModel adminPartyOwner;
            IEnumerable<Budget> budgetsFound;

            budgetsFound = budgets.GetByUserId(UserId);

            adminPartyOwner = new PartyOwnerViewModel();
            adminPartyOwner.AddBudget(budgetsFound);

            return View(adminPartyOwner);
        }
    }
}