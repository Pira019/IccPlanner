﻿

// Ignore Spelling: Prg

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
        public Task<bool> IsServicePrgExist(int tabServiceId, int prgDateId);

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
    }
}
