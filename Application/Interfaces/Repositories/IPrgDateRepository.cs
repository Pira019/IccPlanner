using Application.Dtos;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IPrgDateRepository : IBaseRepository<PrgDate>
    {
        /// <summary>
        ///     Permet de récupérer les Ids de dates des programmes  habituel(Recurring) a partir d’aujourd’hui
        /// </summary>
        /// <param name="prgDateId">
        ///     Id de date de programme
        /// </param>
        /// <returns>
        ///     Id du PrgDepartmentInfos
        /// </returns>
        public Task<IEnumerable<int>> GetRecurringPrgDateIdsFromNowAsync(int prgDateId);

        /// <summary>
        ///     Permet de récupérer les dates des programmes pour un utilisateur spécifique à partir d'une date donnée.
        /// </summary>
        /// <param name="dateFilter">
        ///     Date à partir de laquelle on veut filtrer les dates des programmes.
        ///     Juste le mois et l'année sont pris en compte.
        /// </param>
        /// <param name="userId">
        ///     Id de l'utilisateur pour lequel on veut récupérer les dates des programmes.
        /// </param>
        /// <returns>
        ///     Retourne une collection de dates de type <see cref="DateOnly"/> correspondant aux programmes pour l'utilisateur spécifié.
        /// </returns>
        public Task<IEnumerable<DateOnly>> GetPrgDates(Guid userId, DateOnly dateFilter);
    }
}
