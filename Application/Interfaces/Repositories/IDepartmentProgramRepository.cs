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
        ///     Trouve un DepartmentProgram soft-deleted.
        /// </summary>
        public Task<DepartmentProgram?> FindSoftDeletedAsync(List<int> departmentIds, int programId, bool indRec);

        /// <summary>
        ///     Met à jour un DepartmentProgram.
        /// </summary>
        public Task UpdateAsync(DepartmentProgram entity);

        /// <summary>
        ///     Soft-delete tous les DepartmentProgram liés à un programme.
        /// </summary>
        public Task SoftDeleteByProgramIdAsync(int programId);

        /// <summary>
        ///     Récupère un DepartmentProgram soft-deleted par son ID.
        /// </summary>
        public Task<DepartmentProgram?> GetSoftDeletedByIdAsync(int id);

        /// <summary>
        ///     Supprime définitivement un DepartmentProgram.
        /// </summary>
        public Task HardDeleteAsync(int id);

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
