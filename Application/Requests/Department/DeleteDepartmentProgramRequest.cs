using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Department
{
    /// <summary>
    /// Model du corps pour supprimer un ou plusieurs DepartmentProgram dans la DB
    /// </summary>
    public class DeleteDepartmentProgramRequest
    {
        [Required] 
        public required string DepartmentProgramIds { get; set; }
    }
}
