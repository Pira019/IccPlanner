using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Department : BaseEntity
    {
        public int Id { get; set; }
        public int MinistryId { get; set; }
        public Ministry Ministry { get; set; } = null!;
        [MaxLength(255)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        [MaxLength(55)]
        public string? ShortName { get; set; }
        public DateOnly? StartDate { get; set; } // Date d'ouverture
        public List<Member> Members { get; } = [];
        public List<DepartmentMember> DepartmentMembers { get; } = [];
        public List<Program> Programs { get; } = [];
        public List<DepartmentProgram> DepartmentPrograms { get; } = [];

    }
}
