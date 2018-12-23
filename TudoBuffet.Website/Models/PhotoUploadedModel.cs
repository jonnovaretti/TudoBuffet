using Microsoft.AspNetCore.Http;
using System;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Models
{
    public class PhotoUploadedModel
    {
        public string ThumbnailUrl { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string DeleteType { get; set; }
        public string Type { get; set; }
        public string DeleteUrl { get; set; }
        public long Size { get; set; }
        public Guid Id { get; set; }

        public static PhotoUploadedModel Create(Guid buffetId, Photo photo)
        {
            return new PhotoUploadedModel
            {
                DeleteType = "DELETE",
                Name = photo.DetailFileName,
                DeleteUrl = $"../../../api/admin/buffets/{buffetId.ToString()}/fotos/{photo.Id.ToString()}",
                ThumbnailUrl = photo.ThumbnailUrl,
                Url = photo.DetailUrl,
                Size = photo.Size,
                Id = photo.Id
            };
        }

    }
}
