namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Cette classe definit une personne
    /// </summary>
    public abstract class Person : BaseEntity
    {
        protected string Name { get; set; } 
        protected string FirstName { get; set; } 
        protected string Sexe { get; set; } 
        protected string Tel { get; set; } // numéro de téléphone
        protected string? Email { get; set; } 
        protected string? City { get; set; } 
        protected string? Quarter { get; set; } // Quartier
    }
}
