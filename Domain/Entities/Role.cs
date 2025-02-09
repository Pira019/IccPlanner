using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Represente les roles d'un utilisateur
    /// </summary>
    public class Role : IdentityRole
    { 
        public required string Description { get; set; }  
    }
}
