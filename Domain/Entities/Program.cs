using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Définit les jours des activité ex: cultes, soirée d'adoration etc
    /// </summary>
    public class Program
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? ShortName { get; set; }
        public List<Department> Departments { get; } = [];
        public List<DepartmentProgram> ProgramDepartments { get; } = [];
    }
}
