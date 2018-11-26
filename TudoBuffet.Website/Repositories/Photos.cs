using Microsoft.EntityFrameworkCore;
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

        public async Task SaveAsync(Photo photo)
        {
            mainDbContext.Add(photo);
            mainDbContext.Entry(photo.Buffet).State = EntityState.Detached;
            await mainDbContext.SaveChangesAsync();
        }
    }
}
