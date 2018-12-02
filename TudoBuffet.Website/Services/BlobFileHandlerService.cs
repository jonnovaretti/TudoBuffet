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
    public class BlobFileHandlerService : IBlobFileHandler
    {
        private const int BIG_WIDTH = 700;
        private const int BIG_HEIGHT = 400;
        private const int SMALL_WIDTH = 150;
        private const int SMALL_HEIGHT = 100;
        private readonly IPhotos photos;
        private readonly IOptions<ConnectionString> connectionString;

        public BlobFileHandlerService(IPhotos photos, IOptions<ConnectionString> connectionString)
        {
            this.photos = photos;
            this.connectionString = connectionString;
        }

        public async Task<Photo> Upload(Buffet buffetSelected, IFormFile fileUploaded)
        {
            ImageManipulation imageManipulation;
            MemoryStream bigImageManipulated, smallManipulated;
            BlobAccess blobAccess;
            Photo photo;
            Guid insertedPhotoId;
            string fileName, pathForBigImages, pathForSmallImage, urlFile, urlThumbnail, containerName, completeDirectoryForBigImage, completeDirectoryForSmallImage;

            imageManipulation = new ImageManipulation();
            blobAccess = new BlobAccess(connectionString.Value.BlobStorage);

            containerName = Enum.GetName(typeof(BuffetCategory), buffetSelected.Category).ToLower();
            pathForBigImages = CreateNameBigImageContainer(buffetSelected);
            pathForSmallImage = CreateNameSmallImageContainer(buffetSelected);
            fileName = GenerateFileName(fileUploaded.FileName);

            completeDirectoryForBigImage = string.Concat(pathForBigImages, '/', fileName);
            completeDirectoryForSmallImage = string.Concat(pathForSmallImage, '/', fileName);

            bigImageManipulated = imageManipulation.Resize(fileUploaded.OpenReadStream(), BIG_WIDTH, BIG_HEIGHT);
            urlFile = await blobAccess.UploadToBlob(completeDirectoryForBigImage, containerName, bigImageManipulated);

            smallManipulated = imageManipulation.Resize(fileUploaded.OpenReadStream(), SMALL_WIDTH, SMALL_HEIGHT);
            urlThumbnail = await blobAccess.UploadToBlob(completeDirectoryForSmallImage, containerName, smallManipulated);

            photo = new Photo
            {
                Buffet = buffetSelected,
                CreateAt = DateTime.UtcNow,
                UpdateAt = DateTime.UtcNow,
                Url = urlFile,
                Type = fileUploaded.ContentType,
                Size = fileUploaded.Length,
                ThumbnailUrl = urlThumbnail,
                ContainerName = containerName,
                FileName = completeDirectoryForBigImage,
                ThumbnailName = completeDirectoryForSmallImage,
                IsMainPhoto = false
            };

            insertedPhotoId = await photos.Save(photo);

            return photo;
        }

        public async Task Delete(Photo photo)
        {
            BlobAccess blobAccess;

            blobAccess = new BlobAccess(connectionString.Value.BlobStorage);
            await blobAccess.DeleteFile(photo.FileName, photo.ContainerName);
            await blobAccess.DeleteFile(photo.ThumbnailName, photo.ContainerName);
        }

        private static string GenerateFileName(string fileName)
        {
            return string.Concat(Guid.NewGuid().ToString().Substring(0, 8), fileName.Substring(fileName.IndexOf('.')));
        }

        private static string CreateNameBigImageContainer(Buffet buffetSelected)
        {
            return string.Concat(BIG_WIDTH, 'x', BIG_HEIGHT, '/', buffetSelected.Name.ToLower().Replace(" ", "-"));
        }

        private static string CreateNameSmallImageContainer(Buffet buffetSelected)
        {
            return string.Concat(SMALL_WIDTH, 'x', SMALL_HEIGHT, '/', buffetSelected.Name.ToLower().Replace(" ", "-"));
        }
    }
}
