namespace Application.Responses.Account
{
    /// <summary>
    ///     Représente la réponse pour les revendications d'un compte utilisateur.
    /// </summary>
    public class ClaimsResponse
    {
        /// <summary>
        ///     Roles de l'utilisateur authentifié.
        /// </summary>
        public IEnumerable<string>? Roles { get; set; }

        /// <summary>
        ///     Permissions de l'utilisateur authentifié.
        /// </summary>
        public IEnumerable<string>? Permissions { get; set; }
    }
}
