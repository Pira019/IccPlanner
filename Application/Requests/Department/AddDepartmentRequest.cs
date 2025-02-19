using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Department
{
    /// <summary>
    /// Modelé et validateur pour ajouter un département 
    /// </summary>
    public class AddDepartmentRequest
    {
        [Required]
        public required int MinistryId { get; set; }
        [Required]
        [MaxLength(255)]

        public required string Name { get; set; }
        [MaxLength(55)]

        public string? ShortName { get; set; }
        [Required]

        public required string Description { get; set; }
        [DataType(DataType.Date)]
        public DateOnly? StartDate { get; set; }

    }
}
