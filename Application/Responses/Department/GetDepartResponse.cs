using Application.Dtos.Department;

namespace Application.Responses.Department
{
    /// <summary>
    ///     Modèle de réponse pour la liste des départements.
    /// </summary>
    public class GetDepartResponse
    {
        /// <summary>
        ///     Liste des départements.
        /// </summary>
        public IEnumerable<GetDepartDto>? Departments { get; set; }

        /// <summary>
        ///     Indique si des informations supplémentaires doivent être affichées.
        /// </summary>
        public bool ShowInfo { get; set; }
    }
}
