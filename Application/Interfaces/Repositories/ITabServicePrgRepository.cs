

// Ignore Spelling: Prg

using Application.Requests.ServiceTab;
using Application.Responses.ServicePrg;
using Application.Responses.TabService;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface ITabServicePrgRepository : IBaseRepository<TabServicePrg>
    {
        /// <summary>
        ///     Permet de verifier si un programme a deja un service
        /// </summary>
        /// <param name="tabServiceId">
        ///     Id service voir la table TabService
        /// </param>
        /// <param name="prgDateId">
        ///     Date programme
        /// </param>
        /// <returns>
        ///     Une valeur <see cref="bool"/> 
        /// </returns>
        public Task<bool> IsServicePrgExistAsync(int tabServiceId, int prgDateId);

        /// <summary>
        ///     Permet de récupérer l'id du département par depuis ID de la table TabServicePrg.     
        /// </summary>
        /// <param name="servicePrgId">
        ///     Id du service programme id.
        /// </param>
        /// <returns>
        ///     Id du département. 
        /// </returns>
        public Task<int?> GetDepartmentIdByServicePrgId(int servicePrgId);

        /// <summary>
        ///         Retourne la liste des services d'un programme selon les critères de recherche.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<List<GetServicesListResponse>> GetServicesAsync(ServicesRequest request);
        public Task<GetServiceByDepart?> GetServicePrgByDepart(int idDepart, DateOnly dateOnly, Guid? memberId);

        /// <summary>
        ///     Vérifie que tous les ServicePrg appartiennent bien au département spécifié.
        /// </summary>
        /// <param name="servicePrgIds">Liste des IDs de ServicePrg à vérifier.</param>
        /// <param name="departmentId">ID du département attendu.</param>
        /// <returns>true si tous les ServicePrg appartiennent au département, false sinon.</returns>
        public Task<bool> AllBelongToDepartmentAsync(List<int> servicePrgIds, int departmentId);

        /// <summary>
        ///     Vérifie si au moins un ServicePrg a une date passée.
        /// </summary>
        /// <param name="servicePrgIds">Liste des IDs de ServicePrg à vérifier.</param>
        /// <returns>true si au moins un ServicePrg a une date passée, false sinon.</returns>
        public Task<bool> AnyHasPastDateAsync(List<int> servicePrgIds);
    }
}
