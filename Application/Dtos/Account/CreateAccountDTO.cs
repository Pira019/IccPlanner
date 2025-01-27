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
       public User User { get; set; } 
    }
}
