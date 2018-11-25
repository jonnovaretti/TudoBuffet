using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/area-logada/buffet")]
    [Authorize]
    [ApiController]
    public class BuffetAccountController : LoggedControllerBase
    {
        private const int BIG_WIDTH = 700;
        private const int BIG_HEIGHT = 400;
        private const int SMALL_WIDTH = 150;
        private const int SMALL_HEIGHT = 100;
        private readonly IBuffets buffets;
        private readonly IPhotos photos;
        private readonly IOptions<ConnectionString> config;

        public BuffetAccountController(IBuffets buffets, IPhotos photos, IOptions<ConnectionString> config)
        {
            this.buffets = buffets;
            this.photos = photos;
            this.config = config;
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
            IFormFile fileUploaded;
            ImageManipulation imageManipulation;
            MemoryStream bigImageManipulated, smallManipulated;
            BlobAccess blobAccess;
            Buffet buffetSelected;
            UploadFileResponseModel uploadFileResponseModel;
            Photo photo;
            string fileName, bigContainerName, smallContainerName, urlFile, urlThumbnail;

            try
            {
                buffetSelected = buffets.GetBuffetsById(buffetId);

                if (buffetSelected.Owner.Id != UserId)
                    throw new AccessViolationException("Foto não percetence ao usuário logado: " + UserId);

                imageManipulation = new ImageManipulation();
                blobAccess = new BlobAccess(config.Value.BlobStorage);
                fileUploaded = Request.Form.Files[0];

                fileName = GenerateFileName(fileUploaded.FileName);
                bigContainerName = CreateNameBigImageContainer(buffetSelected);
                smallContainerName = CreateNameSmallImageContainer(buffetSelected);

                bigImageManipulated = imageManipulation.Resize(fileUploaded.OpenReadStream(), BIG_WIDTH, BIG_HEIGHT);
                urlFile = await blobAccess.UploadToBlob(fileName, bigContainerName, bigImageManipulated);

                smallManipulated = imageManipulation.Resize(fileUploaded.OpenReadStream(), SMALL_WIDTH, SMALL_HEIGHT);
                urlThumbnail = await blobAccess.UploadToBlob(fileName, smallContainerName, smallManipulated);

                photo = new Photo {
                    Buffet = new Buffet { Id = Guid.Parse(buffetId) },
                    CreateAt = DateTime.UtcNow,
                    Url = urlFile,
                    UrlThumbnail = urlThumbnail,
                    IsMainPhoto = false
                };

                await photos.SaveAsync(photo);

                uploadFileResponseModel = new UploadFileResponseModel
                {
                    DeleteType = "DELETE",
                    Name = fileName,
                    DeleteUrl = urlFile,
                    ThumbnailUrl = urlThumbnail,
                    Type = fileUploaded.ContentType,
                    Url = urlFile
                };

                return Ok(uploadFileResponseModel);
            }
            catch (Exception ex)
            {
                return ServerError();
            }
        }

        private static string GenerateFileName(string fileName)
        {
            return string.Concat(Guid.NewGuid().ToString().Substring(0, 8), fileName.Substring(fileName.IndexOf('.') + 1));
        }

        private static string CreateNameBigImageContainer(Buffet buffetSelected)
        {
            return string.Concat(Enum.GetName(typeof(BuffetCategory), buffetSelected.Category).ToLower(), '/', BIG_WIDTH, 'x', BIG_HEIGHT, '/', buffetSelected.Name.ToLower().Replace(" ", "-"));
        }

        private static string CreateNameSmallImageContainer(Buffet buffetSelected)
        {
            return string.Concat(Enum.GetName(typeof(BuffetCategory), buffetSelected.Category).ToLower(), '/', BIG_WIDTH, 'x', BIG_HEIGHT, '/', buffetSelected.Name.ToLower().Replace(" ", "-"));
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