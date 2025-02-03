using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Cette classe definit une personne
    /// </summary>
    public abstract class Person : BaseEntity
    {
        [MaxLength(55)]
        public required string Name { get; set; }
        [MaxLength(55)]
        public required string? LastName { get; set; }
        [MaxLength(1)]
        public required string Sexe { get; set; } 
        [MaxLength(55)]       
        public string? City { get; set; }
        [MaxLength(55)]
        public string? Quarter { get; set; } // Quartier
    }
}
