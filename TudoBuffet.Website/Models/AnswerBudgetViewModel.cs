using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class AnswerBudgetViewModel
    {
        public AnswerBudgetViewModel()
        {
            BudgetDetailQuestions = new BudgetDetailQuestionsModel();
        }

        public BudgetDetailQuestionsModel BudgetDetailQuestions { get; set; }

        public string DayParty { get => BudgetDetailQuestions.PartyDay.ToString("dd/MM/yyyy"); set => BudgetDetailQuestions.PartyDay = DateTime.Parse(value); }
        public bool IsAvaliableDate { get => BudgetDetailQuestions.IsDateAvailable.HasValue ? BudgetDetailQuestions.IsDateAvailable.Value : false; set => BudgetDetailQuestions.IsDateAvailable = value; }
        public int QuantityGuests { get => BudgetDetailQuestions.QuantityGuests; set => BudgetDetailQuestions.QuantityGuests = value; }
        public string OwnerPartyName { get => BudgetDetailQuestions.OwnerPartyName; set => BudgetDetailQuestions.OwnerPartyName = value; }
        public List<QuestionsAnswersModel>  Questions { get => BudgetDetailQuestions.Questions; set => BudgetDetailQuestions.Questions = value; }
    }
}
