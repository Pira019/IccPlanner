using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Cette classe definit une personne
    /// </summary>
    public abstract class Person : BaseEntity
    {
        [MaxLength(255)]
        protected string Name { get; set; }
        [MaxLength(255)]
        protected string? LastName { get; set; }
        [MaxLength(10)]
        protected string Sexe { get; set; } 
        [MaxLength(15)]
        protected string Tel { get; set; } // numéro de téléphone
        [MaxLength(255)]
        protected string? Email { get; set; }
        [MaxLength(255)]
        protected string? City { get; set; }
        [MaxLength(255)]
        protected string? Quarter { get; set; } // Quartier
    }
}
