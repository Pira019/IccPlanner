using Application.Requests.Ministry;
using Application.Responses.Ministry;
namespace Application.Interfaces.Services
{
    public interface IMinistryService
    {
        /// <summary>
        /// Modèle de donner pour ajouter un ministère 
        /// </summary>
        /// <param name="ministryRequest"><see cref="AddMinistryRequest"/></param>
        /// <returns></returns>
        public Task<AddMinistryResponse> AddMinistry(AddMinistryRequest ministryRequest);

        /// <summary>
        /// Vérifier si le nom du ministère existe
        /// </summary>
        /// <param name="name">Nom du ministère</param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la valeur <see cref="bool"/> de l'opération </returns>>
        public Task<bool> IsNameMinistryExists(string name);

        /// <summary>
        /// Permet de versifier si le ministère existe 
        /// </summary>
        /// <param name="id">L'Id du ministère</param> 
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant la valeur <see cref="bool"/> de l'opération </returns>>
        public Task<bool> IsMinistryExistsById(int id);
        public Task<IEnumerable<GetMinistriesResponse>> GetAll();
    }
}
