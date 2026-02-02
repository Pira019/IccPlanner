// Ignore Spelling: Prg


using Swashbuckle.AspNetCore.Filters;

namespace Application.Requests.Department
{
    /// <summary>
    ///     Model pour ajouter un programme à plusieurs départements.
    /// </summary>
    public class AddDepartmentProgramRequest
    { 
        public List<int> DepartmentIds { get; set; } 
        public required int ProgramId { get; set; }

        /// <summary>
        ///     Indicates whether the program is recurrent.
        /// </summary>
        public bool IndRecurrent { get; set; }
        public List<int>? Days { get; set; } 
        public List<string>? Dates { get; set; } 
        public string? DateStart { get; set; }  
        public string? DateEnd { get; set; }  
        public string? Comment { get; set; }
    }
}
