using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Cette classe definit une personne
    /// </summary>
    public abstract class Person : BaseEntity
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string? LastName { get; set; }
        [MaxLength(10)]
        public string Sexe { get; set; } 
        [MaxLength(15)]
        public string Tel { get; set; } // numéro de téléphone
        [MaxLength(255)]
        public string? Email { get; set; }
        [MaxLength(55)]
        public string? City { get; set; }
        [MaxLength(55)]
        public string? Quarter { get; set; } // Quartier
    }
}
