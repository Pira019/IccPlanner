using Application.Dtos.Department;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentProgramRepository : IBaseRepository<DepartmentProgram>
    {
        /// <summary>
        /// Récupère les identifiants de programmes existants en base de données.
        /// Cette méthode compare les combinaisons de ProgramId, DepartmentIds et StartAt 
        /// passées avec celles présentes dans la table `DepartmentPrograms`
        /// pour déterminer celles qui existent déjà en BD.
        /// </summary>
        /// <param name="departmentProgram">Liste des DepartmentProgram pour verifier ProgramId, DepartmentIds et StartAt</param>
        /// <returns>Une liste des combinaisons ProgramId, DepartmentIds, et StartAt existantes en base. voir <see cref="DepartmentProgramExistingDto"/></returns>
        public Task<IEnumerable<DepartmentProgramExistingDto>> GetExistingProgramDepartmentsAsync(IEnumerable<DepartmentProgram> departmentProgram);
    }
}
