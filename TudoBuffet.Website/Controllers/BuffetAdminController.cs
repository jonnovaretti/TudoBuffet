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
    [Route("admin/buffets")]
    [Authorize(Roles = "BuffetAdmin")]
    public class BuffetAdminController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotoHandler photoHandlerService;
        private readonly IPlans plans;
        private readonly IPhotos photos;

        public BuffetAdminController(IBuffets buffets, IPhotoHandler photoHandlerService, IPlans plans, IPhotos photos)
        {
            this.buffets = buffets;
            this.photoHandlerService = photoHandlerService;
            this.plans = plans;
            this.photos = photos;
        }

        [HttpGet]
        public ActionResult Index()
        {
            IEnumerable<Plan> plansFound;
            AdminBuffetViewModel newBuffetViewModel;

            plansFound = plans.GetAll();

            newBuffetViewModel = new AdminBuffetViewModel();
            newBuffetViewModel.MapperPlans(plansFound);

            return View(newBuffetViewModel);
        }

        [HttpPost]
        public ActionResult Index(AdminBuffetViewModel newBuffetModel)
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
        [Route("{id}")]
        public ActionResult Edit(string id)
        {
            Buffet buffetFound;
            IEnumerable<Plan> plansFound, planSelected;
            AdminBuffetViewModel newBuffetViewModel;

            buffetFound = buffets.GetBuffetsById(Guid.Parse(id));

            plansFound = plans.GetAll();
            planSelected = plansFound.Where(p => p.Id == buffetFound.PlanSelected.Id);

            newBuffetViewModel = new AdminBuffetViewModel();
            newBuffetViewModel.MapperPlans(planSelected);

            newBuffetViewModel.Buffet = BuffetModel.ToModel(buffetFound);
            newBuffetViewModel.Buffet.Id = buffetFound.Id;

            return View(newBuffetViewModel);
        }

        [HttpPost]
        [Route("{id}")]
        public ActionResult Edit(string id, AdminBuffetViewModel buffetViewModel)
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
        [Route("{id}/fotos")]
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
    }
}