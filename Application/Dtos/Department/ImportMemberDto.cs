
namespace Application.Dtos.Department
{
    /// <summary>
    /// Permet d'organiser l'import
    /// </summary>
    public class ImportMemberDto
    {
        public required string Nom { get; set; }
        public required string Sex { get; set; }
        public required string Contact { get; set; }
        public required int DepartmentId { get; set; }
        public Guid AddedBy { get; set; }

    }
}
