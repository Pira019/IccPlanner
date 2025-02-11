
namespace Application.Dtos.Role
{
    /// <summary>
    /// Model d'objet a retourner pour la lste des Roles
    /// </summary>
    public class GetRolesDto
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
