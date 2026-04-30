using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    ///     Paramètre de l'application stocké en base.
    ///     Clé/valeur flexible pour les configurations dynamiques.
    /// </summary>
    public class AppSettingEntry
    {
        public int Id { get; set; }

        /// <summary>
        ///     Catégorie du paramètre (ex: "deadline").
        /// </summary>
        [MaxLength(50)]
        public string Category { get; set; } = string.Empty;

        /// <summary>
        ///     Clé du paramètre (ex: "global", "program:5", "department:3").
        /// </summary>
        [MaxLength(100)]
        public string Key { get; set; } = string.Empty;

        /// <summary>
        ///     Valeur du paramètre (ex: "3").
        /// </summary>
        [MaxLength(255)]
        public string Value { get; set; } = string.Empty;

        /// <summary>
        ///     Unité (ex: "days", "hours").
        /// </summary>
        [MaxLength(20)]
        public string? Unit { get; set; }

        /// <summary>
        ///     Nom d'affichage (ex: nom du programme ou département).
        /// </summary>
        [MaxLength(255)]
        public string? DisplayName { get; set; }
    }
}
