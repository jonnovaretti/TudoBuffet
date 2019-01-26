using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("orcamento")]
    public class BudgetController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IBudgets budgets;

        public BudgetController(IBuffets buffets, IBudgets budgets)
        {
            this.buffets = buffets;
            this.budgets = budgets;
        }

        [HttpPost]
        [Route("listar")]
        public IActionResult Index(string currentList)
        {
            IEnumerable<Buffet> buffetsFound;
            BudgetSelectedViewModel budgetSelectedViewModel;
            string[] buffetsIds;

            buffetsIds = currentList.Split('|', StringSplitOptions.RemoveEmptyEntries);

            buffetsFound = buffets.GetBuffetsByIds(buffetsIds.ToList());

            budgetSelectedViewModel = new BudgetSelectedViewModel();

            foreach (var buffetFound in buffetsFound)
            {
                budgetSelectedViewModel.AddBuffet(buffetFound);
            }

            return View(budgetSelectedViewModel);
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "UserPartyOwner")]
        public IActionResult Index(BudgetSelectedViewModel budgetSelectedViewModel)
        {
            Budget budget;

            if (!budgetSelectedViewModel.BuffetsBudgetSelected.Any())
                throw new ArgumentException("No mínimo um buffet deve ser selecionado");

            budgetSelectedViewModel.BudgetSent.Validate();

            budget = new Budget();
            budget.QuantityPartyGuests = budgetSelectedViewModel.BudgetSent.QuantityPartyGuests;
            budget.PartyDay = budgetSelectedViewModel.BudgetSent.PartyDay;
            budget.PartyOwner = new UserPartyOwner() { Id = UserId };

            foreach (var buffetSelected in budgetSelectedViewModel.BuffetsBudgetSelected)
            {
                var budgetDetail = new BudgetDetail();

                budgetDetail.Buffet = new Buffet();
                budgetDetail.Buffet.Id = buffetSelected.Id;

                budgetDetail.Questions = new List<BudgetQuestion>();
                foreach (var question in budgetSelectedViewModel.BudgetSent.Questions)
                {
                    budgetDetail.Questions.Add(new BudgetQuestion() { Question = question });
                }

                budget.Details.Add(budgetDetail);
            }

            budgets.Insert(budget);

            return View("Confirm");
        }

        public IActionResult Confirm()
        {
            return View();
        }
    }
}