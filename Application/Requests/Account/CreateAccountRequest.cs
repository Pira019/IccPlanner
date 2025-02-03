 using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Requests.Account
{
    public class CreateAccountRequest
    {
        [MaxLength(55)]
        [Required]
        public required string Name { get; set; }
        [MaxLength(55)]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [MaxLength(1)]
        [Required]
        public required string Sexe { get; set; }
        [MaxLength(15)]
        [MinLength(10)]
        [Phone]
        [DisplayName(("Téléphone"))]
        public string? Tel { get; set; } // numéro de téléphone
       
        [MaxLength(55)]
        public string? City { get; set; }
        [MaxLength(55)]
        public string? Quarter { get; set; } // Quartier   

        /// <summary>
        ///  Mot de passe user
        /// </summary> 
        [DataType(DataType.Password)]
        [Required]
        public required string Password { get; set; }

        /// <summary>
        ///   Mot de passe de confirmation
        /// </summary>
        [Compare("Password")]
        [DataType(DataType.Password)]
        public required string ConfirmPassword { get; set; }   
    }
}
