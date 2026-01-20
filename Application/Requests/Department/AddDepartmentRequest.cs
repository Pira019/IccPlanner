using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Department
{
    /// <summary>
    /// Modelé et validateur pour ajouter un département 
    /// </summary>
    public class AddDepartmentRequest
    { 
        public int MinistryId { get; set; } = default;
        public required string Name { get; set; } 

        public string? ShortName { get; set; }
        public required string Description { get; set; } 
        public DateOnly? StartDate { get; set; }

    }
}
