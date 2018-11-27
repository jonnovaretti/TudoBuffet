using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/area-logada/buffet")]
    [Authorize]
    [ApiController]
    public class BuffetAccountController : LoggedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotos photos;
        private readonly IBlobFileHandler photoHandlerService;

        public BuffetAccountController(IBuffets buffets, IPhotos photos, IBlobFileHandler photoHandlerService)
        {
            this.buffets = buffets;
            this.photos = photos;
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

        [Route("upload-foto")]
        public async Task<ActionResult<UploadFileResponseModel>> PostPhoto([FromQuery]string buffetId)
        {
            List<UploadFileResponseModel> uploadFilesResponseModel;
            IFormFile fileUploaded;
            Buffet buffetSelected;

            try
            {
                buffetSelected = buffets.GetBuffetsById(buffetId);

                if (buffetSelected.Owner.Id != UserId)
                    throw new AccessViolationException("Buffet não percetence ao usuário logado: " + UserId);

                fileUploaded = Request.Form.Files[0];
                var uploadFileResponseModel = await photoHandlerService.Upload(buffetSelected, fileUploaded);

                uploadFilesResponseModel = new List<UploadFileResponseModel>();
                uploadFilesResponseModel.Add(uploadFileResponseModel);

                return Ok(new { Files = uploadFileResponseModel });
            }
            catch (Exception ex)
            {
                return ServerError();
            }
        }

        [Route("delete-photo")]
        public async Task<ActionResult> DeletePhoto([FromQuery] string photoId)
        {
            Photo photoSelected;

            photoSelected = photos.GetById(Guid.Parse(photoId));

            if(photoSelected.Buffet.Owner.Id != UserId)
                throw new AccessViolationException("Photo não percetence ao usuário logado: " + UserId);

            await photoHandlerService.Delete(photoSelected);

            return Ok();
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