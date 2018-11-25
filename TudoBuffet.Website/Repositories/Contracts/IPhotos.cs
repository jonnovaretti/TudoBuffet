using System.Threading.Tasks;
using TudoBuffet.Website.Entities;

namespace TudoBuffet.Website.Repositories.Contracts
{
    public interface IPhotos
    {
        Task SaveAsync(Photo photo);
    }
}
