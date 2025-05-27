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
    }
}
