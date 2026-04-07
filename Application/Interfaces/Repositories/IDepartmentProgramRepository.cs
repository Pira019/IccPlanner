using Application.Dtos.Program;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentProgramRepository : IBaseRepository<DepartmentProgram>
    {
        /// <summary>
        ///     Récupère le département qui a deja un programme .
        /// </summary>
        public Task<DepartmentProgram?> FindDepartmentProgramAsync(List<int> departmentIds, int programId, bool indRec);

        /// <summary>
        ///     Récupère les programmes récurrents actifs pour la génération de dates.
        /// </summary>
        public Task<List<RecurrentProgramDto>> GetRecurrentProgramsForDateGenerationAsync();

        /// <summary>
        ///     Crée les dates récurrentes et copie les services template.
        /// </summary>
        /// <returns>Nombre de dates créées</returns>
        public Task<int> CreateRecurrentDatesWithServicesAsync(List<PrgDate> newDates, List<ServiceTemplateDto> serviceTemplates);
    }
}
