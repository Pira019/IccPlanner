// Ignore Spelling: Prg

using Shared.Enums;

namespace Application.Requests.Department
{
    public class AddDepartmentProgramRequest
    { 
        public required List<int> DepartmentIds { get; set; } 
        public required int ProgramId { get; set; }
        public required string TypePrg { get; set; } 
        public string? Day { get; set; } 
        public List<string>? Date { get; set; }  
        public string? Comment { get; set; }
    }
}
