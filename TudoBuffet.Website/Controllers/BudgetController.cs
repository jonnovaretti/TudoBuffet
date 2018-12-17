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
    public class BudgetController : Controller
    {
        private readonly IBuffets buffets;
        private readonly IBudgets budgets;

        public BudgetController(IBuffets buffets, IBudgets budgets)
        {
            this.buffets = buffets;
            this.budgets = budgets;
        }

        [Route("listar")]
        [HttpPost]
        public IActionResult GetBuffetsByIdList(string currentList)
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
        [Route("enviar")]
        public IActionResult CreateEmailValidation(BudgetSelectedViewModel budgetSelectedViewModel)
        {
            Budget budget;

            if (!budgetSelectedViewModel.BuffetsBudgetSelected.Any())
                throw new ArgumentException("No mínimo um buffet deve ser selecionado");

            budgetSelectedViewModel.BudgetSent.Validate();

            budget = new Budget();
            budgetSelectedViewModel.BuffetsBudgetSelected.ForEach(b => { budget.BudgetBuffets.Add(new BudgetBuffet  () { BuffetId = b.Id }); });
            budget.DayParty = budgetSelectedViewModel.BudgetSent.DayParty;
            budget.EmailSender = budgetSelectedViewModel.BudgetSent.EmailSender;
            budget.Observation = budgetSelectedViewModel.BudgetSent.Observation;
            budget.QuantityPartyGuests = budgetSelectedViewModel.BudgetSent.QuantityPartyGuests;

            budgets.Insert(budget);

            return View();
        }
    }
}