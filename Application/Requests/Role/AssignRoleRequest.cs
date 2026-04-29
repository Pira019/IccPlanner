namespace Application.Requests.Role
{
    /// <summary>
    ///     Modèle pour assigner ou retirer un rôle à un utilisateur.
    /// </summary>
    public class AssignRoleRequest
    {
        /// <summary>
        ///     Id de l'utilisateur.
        /// </summary>
        public string UserId { get; set; } = string.Empty;

        /// <summary>
        ///     Nom du rôle à assigner.
        /// </summary>
        public string RoleName { get; set; } = string.Empty;
    }
}
