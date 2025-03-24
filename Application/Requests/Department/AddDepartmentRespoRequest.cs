using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;

namespace Application.Requests.Department
{
    /// <summary>
    /// Permet d'ajouter un responsable/Referent d'un département
    /// </summary>
    public class AddDepartmentRespoRequest
    {
        [Required]
        [MemberExists]
        public required string MemberId { get; set; }
        [Required] 
        public required int DepartmentId { get; set; }
        public DateOnly? StartAt { get; set; }

        [EndDateAfterStartDate(nameof(StartAt))]
        public DateOnly? EndAt { get; set; } 
    }
}
