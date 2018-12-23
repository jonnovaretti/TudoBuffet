using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;

namespace TudoBuffet.Website.Controllers
{
    [Route("api/admin/buffets")]
    [ApiController]
    [Authorize]
    public class PhotoController : AuthenticatedControllerBase
    {
        private readonly IBuffets buffets;
        private readonly IPhotos photos;
        private readonly IPhotoHandler blobFileService;

        public PhotoController(IBuffets buffets, IPhotos photos, IPhotoHandler blobFileService)
        {
            this.buffets = buffets;
            this.photos = photos;
            this.blobFileService = blobFileService;
        }

        [HttpPost]
        [Route("{buffetId}/fotos")]
        public async Task<ActionResult<List<PhotoUploadedModel>>> PostPhoto(Guid buffetId)
        {
            List<PhotoUploadedModel> photosUploadedModel;
            IFormFile fileUploaded;
            Buffet buffetSelected;
            PhotoUploadedModel photoUploadedModel;

            try
            {
                buffetSelected = buffets.GetBuffetsById(buffetId);

                if (buffetSelected.Owner.Id != UserId)
                    return BadRequest();

                fileUploaded = Request.Form.Files[0];

                if (!fileUploaded.ContentType.Contains("image"))
                    return ServerError();

                var photo = await blobFileService.Upload(buffetSelected, fileUploaded);

                photoUploadedModel = PhotoUploadedModel.Create(buffetId, photo);

                photosUploadedModel = new List<PhotoUploadedModel>();
                photosUploadedModel.Add(photoUploadedModel);

                return Ok(new { Files = photosUploadedModel });
            }
            catch (FileLoadException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu um erro no upload de arquivos, tente novamente");
            }
        }

        [HttpDelete]
        [Route("{buffetid}/fotos/{photoid}")]
        public async Task<ActionResult> DeletePhoto(string buffetId, string photoId)
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