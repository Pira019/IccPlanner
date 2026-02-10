using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    ///     Cette classe représente une invitation dans le système que peut envoyer un utilisateur à un autre.
    /// </summary>
    public class Invitation
    {
        public int Id { get; set; }

        /// <summary>
        ///     Date à laquelle l'invitation a été envoyée.
        /// </summary>
        public DateTime DateSend { get; set; } = DateTime.UtcNow;

        /// <summary>
        ///     Date d'expiration de l'invitation.
        /// </summary>
        public DateTime DateExpiration { get; set; }

        [MaxLength(4)]
        public required String Code { get; set; }

        /// <summary>
        ///     Indique l'identifiant du département associé à l'invitation.
        /// </summary>
        public int DepartmentId { get; set; }

        /// <summary>
        ///     Indique si l'invitation est toujours active.
        /// </summary>
        public bool IndAct { get; set; } = true;

        /// <summary>
        ///     Indique si l'invitation a été utilisée.
        /// </summary>
        public bool IndUsed { get; set; }

        /// <summary>
        ///     Date à laquelle l'invitation a été utilisée.
        /// </summary>
        public DateTime? DateUsed { get; set; }

        /// <summary>
        ///     Email de la personne invitée.
        /// </summary>
        [MaxLength(255)]
        public required string Email { get; set; }

        /// <summary>
        ///     Prenom de la personne invitée.
        /// </summary>
        /// 
        [MaxLength(55)]
        public required string FirstName { get; set; }

        /// <summary>
        ///     Ajouté par l'utilisateur.
        /// </summary>
        public string AddBy { get; set; }

        /// <summary>
        ///    Indentifiant de l'utilisateur qui a mis à jour l'invitation.
        /// </summary>
        public string? UpdatedBy { get; set; } 
}
}
