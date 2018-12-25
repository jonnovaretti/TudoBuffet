using ImageMagick;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;
using TudoBuffet.Website.Configs;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Infrastructures;
using TudoBuffet.Website.Repositories.Contracts;
using TudoBuffet.Website.Services.Contracts;
using TudoBuffet.Website.Tools;

namespace TudoBuffet.Website.Services
{
    public class PhotoHandlerService : IPhotoHandler
    {
        private const int DETAIL_PHOTO_WIDTH = 650;
        private const int DETAIL_PHOTO_HEIGHT = 400;
        private const int DETAIL_PHOTO_WIDTH_MINIMUM = 450;
        private const int DETAIL_PHOTO_HEIGHT_MINIMUM = 200;
        private const int SEARCH_PHOTO_WIDTH = 310;
        private const int SEARCH_PHOTO_HEIGHT = 240;
        private const int THUMBNAIL_PHOTO_WIDTH = 140;
        private const int THUMBNAIL_PHOTO_HEIGHT = 140;
        private readonly IPhotos photos;
        private readonly IOptions<ConnectionString> connectionString;

        public PhotoHandlerService(IPhotos photos, IOptions<ConnectionString> connectionString)
        {
            this.photos = photos;
            this.connectionString = connectionString;
        }

        public async Task<Photo> Upload(Buffet buffetSelected, IFormFile fileUploaded)
        {
            MemoryStream detailPhotoManipulated, thumbnailManipulated, searchPhotoManipulated;
            BlobAccess blobAccess;
            Photo photo;
            Guid insertedPhotoId;
            string fileName, pathForDetailImage, pathForSearchImage, pathForThumbnailImage, detailUrl, thumbnailUrl, searchUrl, containerName, completeDirectoryForDetailImage, completeDirectoryForThumbnailImage, completeDirectoryForSearchImage;

            blobAccess = new BlobAccess(connectionString.Value.BlobStorage);

            fileName = GenerateFileName(fileUploaded.FileName);
            containerName = Enum.GetName(typeof(BuffetCategory), buffetSelected.Category).ToLower();

            pathForSearchImage = CreateNameImageContainer(buffetSelected, SEARCH_PHOTO_WIDTH, SEARCH_PHOTO_HEIGHT);
            pathForDetailImage = CreateNameImageContainer(buffetSelected, DETAIL_PHOTO_WIDTH, DETAIL_PHOTO_HEIGHT);
            pathForThumbnailImage = CreateNameImageContainer(buffetSelected, THUMBNAIL_PHOTO_WIDTH, THUMBNAIL_PHOTO_HEIGHT);

            completeDirectoryForSearchImage = string.Concat(pathForSearchImage, '/', fileName);
            completeDirectoryForDetailImage = string.Concat(pathForDetailImage, '/', fileName);
            completeDirectoryForThumbnailImage = string.Concat(pathForThumbnailImage, '/', fileName);

            searchPhotoManipulated = ManipulatePhoto(fileUploaded.OpenReadStream(), SEARCH_PHOTO_WIDTH, SEARCH_PHOTO_HEIGHT);
            searchUrl = await blobAccess.UploadToBlob(completeDirectoryForSearchImage, containerName, searchPhotoManipulated);

            detailPhotoManipulated = ManipulatePhotoDetail(fileUploaded.OpenReadStream(), DETAIL_PHOTO_WIDTH, DETAIL_PHOTO_HEIGHT, DETAIL_PHOTO_WIDTH_MINIMUM, DETAIL_PHOTO_HEIGHT_MINIMUM);
            detailUrl = await blobAccess.UploadToBlob(completeDirectoryForDetailImage, containerName, detailPhotoManipulated);

            thumbnailManipulated = ManipulatePhoto(fileUploaded.OpenReadStream(), THUMBNAIL_PHOTO_WIDTH, THUMBNAIL_PHOTO_HEIGHT);
            thumbnailUrl = await blobAccess.UploadToBlob(completeDirectoryForThumbnailImage, containerName, thumbnailManipulated);

            photo = new Photo
            {
                Buffet = buffetSelected,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Type = fileUploaded.ContentType,
                Size = fileUploaded.Length,
                ContainerName = containerName,
                ThumbnailUrl = thumbnailUrl,
                ThumbnailFileName = completeDirectoryForThumbnailImage,
                DetailUrl = detailUrl,
                DetailFileName = completeDirectoryForDetailImage,
                SearchUrl = searchUrl,
                SearchFileName = completeDirectoryForSearchImage
            };

            insertedPhotoId = await photos.Save(photo);

            return photo;
        }

        public async Task Delete(Photo photo)
        {
            BlobAccess blobAccess;

            blobAccess = new BlobAccess(connectionString.Value.BlobStorage);
            await blobAccess.DeleteFile(photo.DetailFileName, photo.ContainerName);
            await blobAccess.DeleteFile(photo.ThumbnailFileName, photo.ContainerName);
            await blobAccess.DeleteFile(photo.SearchFileName, photo.ContainerName);
        }

        private MemoryStream ManipulatePhoto(Stream file, int width, int height)
        {
            MemoryStream fileManipulated;
            ImageManipulator imageManipulation;

            imageManipulation = ImageManipulator.CreateImageManipulation(file);

            if (imageManipulation.Height > imageManipulation.Width)
            {
                fileManipulated = imageManipulation.Resize(height, width);
            }
            else
            {
                fileManipulated = imageManipulation.Resize(width, height);
            }

            return fileManipulated;
        }

        private MemoryStream ManipulatePhotoDetail(Stream file, int widthMaximum, int heightMaximum, int widthMinimum, int heightMinimum)
        {
            PhotoAppraiser photoAppraiser;
            MemoryStream fileManipulated;
            ImageManipulator imageManipulation;
            AppraisalResult appraisalResult;

            photoAppraiser = new PhotoAppraiser(widthMaximum, heightMaximum, widthMinimum, heightMinimum);
            imageManipulation = ImageManipulator.CreateImageManipulation(file);

            appraisalResult = photoAppraiser.AppraiseSize(imageManipulation.Width, imageManipulation.Height);

            if (appraisalResult == AppraisalResult.ShouldBeShortenIgnoringRatio)
            {
                fileManipulated = imageManipulation.ResizeIgnoringRatio(widthMaximum, heightMaximum);
            }
            else if (appraisalResult == AppraisalResult.ShouldBeShortenKeepingRatio)
            {
                fileManipulated = imageManipulation.Resize(widthMaximum, heightMaximum);
            }
            else if (appraisalResult == AppraisalResult.Keep)
            {
                fileManipulated = imageManipulation.Resize(imageManipulation.Width, imageManipulation.Height);
            }
            else
            {
                throw new FileLoadException($"Tamanho da imagem é muito pequeno. O minimo é de largura {widthMinimum} e altura {heightMinimum}");
            }

            return fileManipulated;
        }

        private static string GenerateFileName(string fileName)
        {
            return string.Concat(Guid.NewGuid().ToString().Substring(0, 8), ".jpg");
        }

        private static string CreateNameImageContainer(Buffet buffetSelected, int width, int height)
        {
            return string.Concat(width, 'x', height, '/', buffetSelected.Name.ToLower().Replace(" ", "-"));
        }
    }
}
