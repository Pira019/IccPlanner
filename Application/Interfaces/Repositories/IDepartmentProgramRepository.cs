using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentProgramRepository : IBaseRepository<DepartmentProgram>
    {
        /// <summary>
        ///     Récupère le département qui a deja un programme 
        /// </summary>
        /// <param name="departmentIds">
        ///     Ids des départements    
        /// </param>
        /// <param name="programId">
        ///     Id de programme
        /// </param>
        /// <param name="programType">
        ///     Type de programme
        /// </param>
        /// <returns>
        ///     Retourne <see cref="DepartmentProgram"/>
        /// </returns>
        public Task<DepartmentProgram?> GetFirstExistingDepartmentProgramAsync(List<int> departmentIds,int programId, string programType);
    }
}
