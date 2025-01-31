using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    /// <summary>
    /// Represente les roles d'un utilisateur
    /// </summary>
    public class Role : IdentityRole
    { 
        public string Description { get; set; }  
    }
}
