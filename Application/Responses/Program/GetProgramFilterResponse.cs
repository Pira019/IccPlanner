using Application.Dtos.Department;
using Application.Dtos.PrgDate;

namespace Application.Responses.Program
{
    /// <summary>
    /// Représente la réponse pour récupérer les programmes avec un filtre spécifique.
    /// </summary>
    public class GetProgramFilterResponse
    {
        public string? Name { get; set; }
        public bool? IndRecurent { get; set; }
        public IEnumerable<DepartmentDto>? Departments { get; set; }
        public IEnumerable<PrgDateDto>? Dates { get; set; }

    }
}
