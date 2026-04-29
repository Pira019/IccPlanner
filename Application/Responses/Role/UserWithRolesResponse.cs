namespace Application.Responses.Role
{
    /// <summary>
    ///     Utilisateur avec ses rôles.
    /// </summary>
    public class UserWithRolesResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public List<string> Roles { get; set; } = [];
    }
}
