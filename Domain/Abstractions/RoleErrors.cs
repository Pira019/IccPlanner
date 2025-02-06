namespace Domain.Abstractions
{
    /// <summary>
    /// Message d'erreur pour le Role
    /// </summary>
    public static class RoleErrors
    {
        /// <summary>
        /// Ce message s'affiche dans le log si il ya une erreur inconnue lors de la creation d'un role
        /// </summary>
        static public readonly Error ERROR_CREATE_ROLE = new("ERROR_CREATE_ROLE", "An unexpected error occurred while creating the role."); 
    }
}
