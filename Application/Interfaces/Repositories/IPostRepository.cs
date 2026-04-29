using Application.Responses.Department;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPostRepository
    {
        public Task<Poste?> FindPosteByName(string name);
        public Task<Poste> InsertAsync(Poste poste);
        public Task<Poste?> GetByIdAsync(int id);
        public Task<List<PosteResponse>> GetAllAsync();
        public Task UpdateAsync(Poste poste);
        public Task DeleteAsync(int id);
        public Task<bool> IsNameExistsAsync(string name);

        /// <summary>
        ///     Récupère tous les ShortName de postes existants.
        /// </summary>
        public Task<List<string>> GetAllShortNamesAsync();

        /// <summary>
        ///     Insère plusieurs postes en une seule opération.
        /// </summary>
        public Task InsertRangeAsync(List<Poste> postes);
    }
}
