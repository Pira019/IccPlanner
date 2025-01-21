 using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Application.Requests.Account
{
    public class CreateAccountRequest
    {
        [MaxLength(55)]
        [Required]
        public string Name { get; set; }
        [MaxLength(55)]
        public string? LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(10)]
        [Required]
        public string Sexe { get; set; }
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
        public string Password { get; set; }

        /// <summary>
        ///   Mot de passe de confirmation
        /// </summary>
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }   
    }
}
