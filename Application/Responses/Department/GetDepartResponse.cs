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
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
