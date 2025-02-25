
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        public Task<Poste?> FindPosteByName(string name);
    }
}
