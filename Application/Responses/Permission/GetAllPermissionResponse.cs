namespace Application.Responses.Permission
{
    /// <summary>
    ///     Classe de réponse pour obtenir toutes les permissions.
    /// </summary>
    public class GetAllPermissionResponse
    {
        /// <summary>
        ///     Identifiant unique de la permission.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     Nom de la permission.
        /// </summary>
        public string Name { get; set; } = default!;

        /// <summary>
        ///     Description de la permission.
        /// </summary>
        public string? Description { get; set; }
    }
}
