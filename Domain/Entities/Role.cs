using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Représente les roles d'un utilisateur
    /// </summary>
    public class Role : IdentityRole
    { 
        public required string Description { get; set; }  
    }
}
