using Application.Requests.Program;
using Application.Responses.Program;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProgramRepository : IBaseRepository<Program>
    {
        /// <summary>
        /// Vérifier si le nom du programme existe
        /// </summary>
        /// <param name="name">Nom du programme</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la valeur <see cref="bool"/> de l'opération </returns>>
        public Task<bool> IsNameExistsAsync(string name);

        /// <summary>
        ///     Récupérer tous les programmes par un filter <see cref="GetProgramFilterRequest"/>
        /// </summary>
        /// <param name="getProgramFilter">
        ///     Filtre pour récupérer les programmes
        /// </param>
        /// <returns></returns>
        public Task<IEnumerable<GetProgramFilterResponse>> GetProgramFilterAsync(GetProgramFilterRequest getProgramFilter);
    }
}
