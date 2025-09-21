 namespace Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface pour gérer les réclamations (claims) des utilisateurs.
    /// </summary>
    public interface IClaimRepository 
    {
        /// <summary>
        ///    Récupère les valeurs des claims associées à un utilisateur spécifique par son ID.
        /// </summary>
        /// <param name="userId">
        ///     User Id de l'utilisateur dont on veut obtenir les valeurs des claims.
        /// </param>
        /// <returns>
        ///     Ils retournent une collection de valeurs de claims associées à l'utilisateur.
        /// </returns>
        public Task<IEnumerable<String?>> GetClaimsValuesByUserIdAsync(string userId);
    }
}
