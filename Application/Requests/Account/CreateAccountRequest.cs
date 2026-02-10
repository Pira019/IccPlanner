using System.ComponentModel;
 using System.ComponentModel.DataAnnotations;
using Shared.Ressources;

namespace Application.Requests.Account
{
    public class CreateAccountRequest
    { 
        public required string Name { get; set; } 
        public string? LastName { get; set; } 
        public string? Email { get; set; } 
        public required string Sexe { get; set; } 
        public string? Tel { get; set; } // numéro de téléphone 
        public string? City { get; set; } 
        public string? Quarter { get; set; } // Quartier   

        /// <summary>
        ///  Mot de passe user
        /// </summary>  
        public required string Password { get; set; }

        /// <summary>
        ///   Mot de passe de confirmation
        /// </summary>  
        public required string ConfirmPassword { get; set; }   
    }
}
