using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TudoBuffet.Website.Entities;
using TudoBuffet.Website.Repositories.Context;
using TudoBuffet.Website.Repositories.Contracts;

namespace TudoBuffet.Website.Repositories
{
    public class Photos : IPhotos
    {
        private readonly MainDbContext mainDbContext;

        public Photos(MainDbContext mainDbContext)
        {
            this.mainDbContext = mainDbContext;
        }

        public async Task Delete(Photo photo)
        {
            mainDbContext.Remove(photo);
            await mainDbContext.SaveChangesAsync();
        }

        public async Task<Photo> GetById(Guid fileId)
        {
            Photo photoFound;

            photoFound = await mainDbContext.Photos.Include(p => p.Buffet).Include(p => p.Buffet.Owner).FirstOrDefaultAsync(p => p.Id == fileId);

            return photoFound;
        }

        public IEnumerable<Photo> GetPhotosByBuffet(Guid buffetId)
        {
            IEnumerable<Photo> photosFound;

            photosFound = mainDbContext.Photos.Include(p => p.Buffet).Include(p => p.Buffet.Owner).Where(p => p.Buffet.Id == buffetId).ToList();

            return photosFound;
        }

        public async Task<Guid> Save(Photo photo)
        {
            mainDbContext.Add(photo);
            mainDbContext.Entry(photo.Buffet).State = EntityState.Detached;
            await mainDbContext.SaveChangesAsync();

            return photo.Id;
        }
    }
}
