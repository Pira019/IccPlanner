using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Définit le poste qu'un membre peut avoir dans un département 
    /// </summary>
    public class Poste
    {
        public int Id { get; set; }
        [MaxLength(55)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        [MaxLength(15)]
        public string? ShortName { get; set; }

        /// <summary>
        ///     Indique si le poste confère le droit de gérer des membres dans le département.
        /// </summary>
        public bool IndGest { get; set; } = default;
    }
}
