using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "UserBuffetAdmin")]
    public class BuffetAdminController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotoHandler photoHandlerService;
        private readonly IPlans plans;
        private readonly IPhotos photos;
        private readonly IBudgets budgets;

        public BuffetAdminController(IBuffets buffets, IPhotoHandler photoHandlerService, IPlans plans, IPhotos photos, IBudgets budgets)
        {
            this.buffets = buffets;
            this.photoHandlerService = photoHandlerService;
            this.plans = plans;
            this.photos = photos;
            this.budgets = budgets;
        }

        public IActionResult Home()
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

        [HttpGet]
        [Route("buffets")]
        public ActionResult Index()
        {
            IEnumerable<Plan> plansFound;
            BuffetViewModel newBuffetViewModel;

            plansFound = plans.GetAll();

            newBuffetViewModel = new BuffetViewModel();
            newBuffetViewModel.MapperPlans(plansFound);

            return View(newBuffetViewModel);
        }

        [HttpPost]
        [Route("buffets")]
        public ActionResult Index(BuffetViewModel newBuffetModel)
        {
            Buffet buffet;
            Guid buffetId;

            try
            {
                newBuffetModel.Buffet.Validate();
                buffet = newBuffetModel.Buffet.ToEntity(UserId);

                buffetId = buffets.Save(buffet);

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return ServerError();
            }
        }

        [HttpGet]
        [Route("buffets/{id}")]
        public ActionResult Edit(string id)
        {
            Buffet buffetFound;
            IEnumerable<Plan> plansFound, planSelected;
            BuffetViewModel newBuffetViewModel;

            buffetFound = buffets.GetBuffetsById(Guid.Parse(id));

            plansFound = plans.GetAll();
            planSelected = plansFound.Where(p => p.Id == buffetFound.PlanSelected.Id);

            newBuffetViewModel = new BuffetViewModel();
            newBuffetViewModel.MapperPlans(planSelected);

            newBuffetViewModel.Buffet = BuffetModel.ToModel(buffetFound);
            newBuffetViewModel.Buffet.Id = buffetFound.Id;

            return View(newBuffetViewModel);
        }

        [HttpPost]
        [Route("buffets/{id}")]
        public ActionResult Edit(string id, BuffetViewModel buffetViewModel)
        {
            Buffet buffet;
            Guid buffetId;

            try
            {
                buffetViewModel.Buffet.Validate();
                buffet = buffetViewModel.Buffet.ToEntity(UserId);

                buffetId = buffets.Update(Guid.Parse(id), buffet);

                return RedirectToAction("Index", "Admin");
            }
            catch (Exception ex)
            {
                return ServerError();
            }
        }

        [HttpGet]
        [Route("buffets/{id}/fotos")]
        public ActionResult Photos(Guid id)
        {
            PhotoDetailViewModel photoDetail;
            List<PhotoUploadedModel> photosUploadedModel;
            IEnumerable<Photo> photosFound;
            Buffet buffetFound;

            buffetFound = buffets.GetBuffetsById(id);

            if (buffetFound.Owner.Id != UserId)
                return BadRequest();

            photosFound = photos.GetPhotosByBuffet(id);

            photosUploadedModel = new List<PhotoUploadedModel>();

            foreach (var photoFound in photosFound)
            {
                var photoUploadedModel = PhotoUploadedModel.Create(id, photoFound);

                photosUploadedModel.Add(photoUploadedModel);
            }

            photoDetail = new PhotoDetailViewModel();
            photoDetail.BuffetId = id;
            photoDetail.Photos = photosUploadedModel;

            return View(photoDetail);
        }

        [HttpGet]
        [Route("budgets")]
        public ActionResult Bugets()
        {
            IEnumerable<Budget> budgetsFound;
            List<ReceivedBudgetModel> receivedBudgets;
            ReceivedBudgetViewModel receivedBudgetViewModel;

            budgetsFound = budgets.GetByOwnerBuffet(UserId);

            receivedBudgets = new List<ReceivedBudgetModel>();

            foreach (var budgetFound in budgetsFound)
            {
                var receivedBudget = new ReceivedBudgetModel
                {
                    BudgetDetailId = budgetFound.Details.First().Id,
                    OwnerPartyName = budgetFound.PartyOwner.Name,
                    PartyDay = budgetFound.PartyDay,
                    QuantityPartyGuests = budgetFound.QuantityPartyGuests,
                    SentAt = budgetFound.CreateAt,
                    WasAnswered = budgetFound.Details.First().AnsweredAt.HasValue
                };

                receivedBudgets.Add(receivedBudget);
            }

            receivedBudgetViewModel = new ReceivedBudgetViewModel();
            receivedBudgetViewModel.ReceivedBudgets = receivedBudgets;

            return View(receivedBudgetViewModel);
        }

        [HttpGet]
        [Route("budgets/{budgetdetailid}")]
        public ActionResult BudgetDetail(Guid budgetdetailid)
        {
            AnswerBudgetViewModel answerBudget;
            BudgetDetailQuestionsModel budgetDetailQuestions;
            Budget budgetFound;

            budgetFound = budgets.GetById(budgetdetailid);

            if (!budgetFound.BelongsToBuffetAdmin(UserId))
                return ServerError();

            budgetDetailQuestions = new BudgetDetailQuestionsModel()
            {
                BudgetDetailId = budgetFound.Details.First().Id,
                IsDateAvailable = budgetFound.Details.First().IsDateAvaliable,
                OwnerPartyName = budgetFound.PartyOwner.Name,
                PartyDay = budgetFound.PartyDay,
                ProposedDateFor = budgetFound.Details.First().ProposedDateFor,
                QuantityGuests = budgetFound.QuantityPartyGuests
            };

            foreach (var question in budgetFound.Details.First().Questions)
            {
                var questionAnswred = new QuestionsAnswersModel();

                questionAnswred.Question = question.Question;
                questionAnswred.QuestionId = question.Id;

                foreach (var answer in question.Answers)
                    questionAnswred.Answer = answer.Answer;

                budgetDetailQuestions.Questions.Add(questionAnswred);
            }

            answerBudget = new AnswerBudgetViewModel();
            answerBudget.BudgetDetailQuestions = budgetDetailQuestions;

            return View(answerBudget);
        }
    }
}