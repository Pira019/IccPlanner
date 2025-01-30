using System.ComponentModel.DataAnnotations;
using Application.Helper.Validators;

namespace Application.Requests.Role
{
    /// <summary>
    /// Model de donnees a envoyer pour creer un role
    /// </summary>
    public class CreateRoleRequest
    {
        /// <summary>
        /// Nom du role
        /// </summary>
        [Required]
        [LettersOnly]
        public string Name { get; set; } 
        /// <summary>
        /// Description du Role
        /// </summary>
        public string Description { get; set; }

    }
}
