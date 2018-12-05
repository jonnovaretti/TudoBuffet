using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/admin/fotos")]
    [ApiController]
    public class PhotoController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotos photos;
        private readonly IBlobFileHandler blobFileService;

        public PhotoController(IBuffets buffets, IPhotos photos, IBlobFileHandler blobFile)
        {
            this.buffets = buffets;
            this.photos = photos;
            this.blobFileService = blobFile;
        }

        [HttpGet]
        public ActionResult<List<PhotoUploadedModel>> GetPhotosByBuffet([FromQuery] Guid buffetId)
        {
            List<PhotoUploadedModel> photosUploadedModel;
            IEnumerable<Photo> photosFound;
            Buffet buffetFound;

            buffetFound = buffets.GetBuffetsById(buffetId);

            if (buffetFound.Owner.Id != UserId)
                return BadRequest();

            photosFound = photos.GetPhotosByBuffetAsync(buffetId);

            photosUploadedModel = new List<PhotoUploadedModel>();

            foreach (var photoFound in photosFound)
            {
                var photoUploadedModel = new PhotoUploadedModel
                {
                    DeleteType = "DELETE",
                    Name = photoFound.FileName,
                    DeleteUrl = "api/admin/fotos?photoid=" + photoFound.Id.ToString(),
                    ThumbnailUrl = photoFound.ThumbnailUrl,
                    Url = photoFound.Url,
                    Size = photoFound.Size,
                    Id = photoFound.Id
                };

                photosUploadedModel.Add(photoUploadedModel);
            }

            return Ok(photosUploadedModel);
        }

        [HttpPost]
        public async Task<ActionResult<List<PhotoUploadedModel>>> PostPhoto([FromQuery]string buffetId)
        {
            List<PhotoUploadedModel> photosUploadedModel;
            IFormFile fileUploaded;
            Buffet buffetSelected;

            try
            {
                buffetSelected = buffets.GetBuffetsById(Guid.Parse(buffetId));

                if (buffetSelected.Owner.Id != UserId)
                    return BadRequest();

                fileUploaded = Request.Form.Files[0];

                if (!fileUploaded.ContentType.Contains("image"))
                    return ServerError();

                var photo = await blobFileService.Upload(buffetSelected, fileUploaded);

                var photoUploadedModel = new PhotoUploadedModel
                {
                    DeleteType = "DELETE",
                    Name = photo.FileName,
                    DeleteUrl = "api/admin/fotos?photoid=" + photo.Id.ToString(),
                    ThumbnailUrl = photo.ThumbnailUrl,
                    Type = fileUploaded.ContentType,
                    Url = photo.Url,
                    Size = fileUploaded.Length,
                    Id = photo.Id
                };

                photosUploadedModel = new List<PhotoUploadedModel>();
                photosUploadedModel.Add(photoUploadedModel);

                return Ok(new { Files = photosUploadedModel });
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro no upload de arquivos, tente novamente");
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePhoto([FromQuery] string photoId)
        {
            Photo photoSelected;

            photoSelected = await photos.GetById(Guid.Parse(photoId));

            if (photoSelected.Buffet.Owner.Id != UserId)
                return BadRequest();

            await blobFileService.Delete(photoSelected);
            await photos.Delete(photoSelected);

            return Ok();
        }

    }
}