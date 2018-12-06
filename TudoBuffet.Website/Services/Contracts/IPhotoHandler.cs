using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Models;

namespace TudoBuffet.Website.Services.Contracts
{
    public interface IPhotoHandler
    {
        Task<Photo> Upload(Buffet buffet, IFormFile fileUploaded);
        Task Delete(Photo photo);
    }
}
