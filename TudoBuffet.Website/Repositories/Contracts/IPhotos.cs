using System;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IPhotos
    {
        Task<Guid> Save(Photo photo);
        Task<Photo> GetById(Guid fileId);
    }
}
