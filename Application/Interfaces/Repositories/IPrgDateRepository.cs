using Application.Dtos;
using Application.Responses.Program;
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
        ///     Obtient une liste de dates de programmes pour un département, un mois et une année donnés.
        /// </summary>
        /// <param name="idDepart"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns>
        ///     Retourne une liste de dates de programmes correspondant au département, au mois et à l'année spécifiés. Chaque date représente un jour où un programme est prévu pour le département donné.
        /// </returns>
        public Task<IEnumerable<DateOnly>> GetPrgServiceDatesAsync(int idDepart, int month, int year);


        /// <summary>
        ///     Obtient une liste de programmes pour un mois et une année donnés.
        /// </summary>
        /// <param name="month">   
        ///     Mois pour lequel récupérer les programmes.
        /// </param>
        /// <param name="year">
        ///     Année pour laquelle récupérer les programmes.
        /// </param>
        /// <returns>
        ///     Liste des programmes correspondant au mois et à l'année spécifiés.
        /// </returns>
        public Task<GetPrg> GetByMonthYearAsync(int month, int year);
    }
}
