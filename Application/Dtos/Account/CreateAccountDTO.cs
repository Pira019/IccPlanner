using Domain.Entities;

namespace Application.Dtos.Account
{
    /// <summary>
    ///   Modele de donnee pour creer un compte
    /// </summary>
    public class CreateAccountDto
    {
        /// <summary>
        ///   Compte 
        /// </summary>
       public required User User { get; set; } 
    }
}
