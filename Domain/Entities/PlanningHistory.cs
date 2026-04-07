using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities
{
    /// <summary>
    ///     Historique des actions effectuées sur le planning (assignation, retrait, modification).
    /// </summary>
    public class PlanningHistory
    {
        public int Id { get; set; }

        /// <summary>
        ///     Id du planning concerné.
        /// </summary>
        public int PlanningId { get; set; }

        /// <summary>
        ///     Action effectuée (Assigned, Unassigned, Updated).
        /// </summary>
        public PlanningAction Action { get; set; }

        /// <summary>
        ///     Nom du membre concerné (dénormalisé pour l'historique).
        /// </summary>
        [MaxLength(255)]
        public required string MemberName { get; set; }

        /// <summary>
        ///     Date du programme concerné.
        /// </summary>
        public DateOnly ProgramDate { get; set; }

        /// <summary>
        ///     Nom du poste au moment de l'action (nullable).
        /// </summary>
        [MaxLength(55)]
        public string? PosteName { get; set; }

        /// <summary>
        ///     Type de planning au moment de l'action.
        /// </summary>
        public PlanningType PlanningType { get; set; }

        /// <summary>
        ///     Commentaire au moment de l'action.
        /// </summary>
        public string? Comment { get; set; }

        /// <summary>
        ///     Qui a effectué l'action.
        /// </summary>
        public Guid ActionById { get; set; }
        public Member ActionBy { get; set; } = null!;

        /// <summary>
        ///     Date et heure de l'action.
        /// </summary>
        public DateTime ActionAt { get; set; } = DateTime.UtcNow;
    }
}
