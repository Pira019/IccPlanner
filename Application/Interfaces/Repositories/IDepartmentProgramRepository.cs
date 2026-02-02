using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentProgramRepository : IBaseRepository<DepartmentProgram>
    {
        /// <summary>
        ///     Récupère le département qui a deja un programme .
        /// </summary>
        /// <param name="departmentIds">
        ///     Departements à vérifier.
        /// </param>
        /// <param name="programId">
        ///     Programme à vérifier.
        /// </param>
        /// <param name="indRec">
        ///     Indicateur de récurrence.
        /// </param>
        /// <returns>
        ///     Retourne le premier département qui a déjà le programme spécifié, ou null s'il n'existe pas.
        /// </returns>
        public Task<DepartmentProgram?> FindDepartmentProgramAsync(List<int> departmentIds,int programId, bool indRec);
    }
}
