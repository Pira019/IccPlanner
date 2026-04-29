using Microsoft.AspNetCore.Http;

namespace Application.Requests.Invitation
{
    /// <summary>
    ///     Modèle pour importer des invitations en masse via un fichier Excel.
    ///     Le fichier doit contenir les colonnes : Prenom, Email.
    /// </summary>
    public class BulkInviteRequest
    {
        /// <summary>
        ///     Fichier Excel (.xlsx) contenant les colonnes Prenom et Email.
        /// </summary>
        public IFormFile File { get; set; } = null!;

        /// <summary>
        ///     Id du département auquel les membres seront invités.
        /// </summary>
        public int DepartmentId { get; set; }
    }
}
