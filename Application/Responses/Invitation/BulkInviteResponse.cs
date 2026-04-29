namespace Application.Responses.Invitation
{
    /// <summary>
    ///     Résultat de l'import en masse d'invitations.
    /// </summary>
    public class BulkInviteResponse
    {
        /// <summary>
        ///     Nombre d'invitations envoyées avec succès.
        /// </summary>
        public int Sent { get; set; }

        /// <summary>
        ///     Nombre d'invitations ignorées (email déjà utilisé, doublon, etc.).
        /// </summary>
        public int Skipped { get; set; }

        /// <summary>
        ///     Détails des lignes ignorées.
        /// </summary>
        public List<string> Errors { get; set; } = [];
    }
}
