using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;

namespace Application.Requests.Department
{
    public class AddDepartmentProgramRequest
    {
        [Required]
        [DepartmentIdListExists]
        public required string DepartmentIds { get; set; }

        [Required]
        [Range(1,int.MaxValue)]
        [ProgramExists]
        public required int ProgramId { get; set; }
        [DataType(DataType.Date)]
        public required DateOnly StartAt { get; set; }

        [DataType(DataType.Date)]
        [EndDateAfterStartDate(nameof(StartAt))]
        public DateOnly? EndAt { get; set; }
        [MaxLength(255)]
        public string? Localisation { get; set; }
        public string? Comment { get; set; }
    }
}
