// Ignore Spelling: Prg
 

namespace Application.Requests.Department
{
    /// <summary>
    ///     Requête pour ajouter un programme à un département.
    /// </summary>
    public class AddDepartmentProgramRequest
    { 
        public required List<int> DepartmentIds { get; set; } 
        public required int ProgramId { get; set; }
        public required bool IndRecurent { get; set; } 
        public List<string>? Days { get; set; } 
        public List<string>? Dates { get; set; }  
        public string? Comment { get; set; }
    }
}
