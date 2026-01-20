using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface pour le dépôt des membres de département.
    /// </summary>
    public interface IDepartmentMemberRepository : IBaseRepository<DepartmentMember>
    {
        /// <summary>
        ///   Permet d'obtenir Id de la table si un membre est dans un département spécifique.
        /// </summary>
        /// <param name="departmentId">
        ///     ID du département à vérifier.
        /// </param>
        /// <param name="memberId">
        ///  ID du membre à vérifier.
        /// </param>
        /// <returns>
        ///     un objet <see cref="Task"/> qui représente l'opération asynchrone,
        ///     et qui contient un booléen indiquant si le membre est dans le département.  
        /// </returns>
        public Task<int?> GetMemberInDepartmentIdAsync(int? departmentId, Guid memberId);

        /// <summary>
        ///     Indique si un utilisateur occupe un poste qui lui confère le droit de gérer des membres dans au moins un département.
        ///     Ex : Chef de département, Assistant chef de département, Coordinateur de département.
        /// </summary>
        /// <param name="userId">
        ///     Indique l'ID de l'utilisateur à vérifier.
        /// </param>
        /// <returns>
        ///     Retourne un objet <see cref="Task"/> qui représente l'opération asynchrone,
        /// </returns>
        public Task<bool> CanManageMembersAsync(string userId);
    }
}
