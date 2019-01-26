using System;
using System.Collections.Generic;

namespace TudoBuffet.Website.Models
{
    public class BudgetDetailQuestionsModel
    {
        public int QuantityGuests { get; set; }
        public DateTime PartyDay { get; set; }
        public bool? IsDateAvailable { get; set; }
        public DateTime? ProposedDateFor { get; set; }
        public Guid BudgetDetailId { get; set; }
        public string OwnerPartyName { get; set; }
        public List<QuestionsAnswersModel> Questions { get; set; }

        public BudgetDetailQuestionsModel()
        {
            Questions = new List<QuestionsAnswersModel>();
        }
    }

    public class QuestionsAnswersModel
    {
        public Guid QuestionId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
