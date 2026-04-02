using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Department
{
    public class AssignPostesRequest
    {
        [Required]
        public required List<int> PosteIds { get; set; }
    }
}
