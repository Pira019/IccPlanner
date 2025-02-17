using Domain.Entities;

namespace Application.Dtos.Account
{
    /// <summary>
    ///   Modèle de donnée pour créer un compte
    /// </summary>
    public class CreateAccountDto
    {
        /// <summary>
        ///   Compte 
        /// </summary>
       public User? User { get; set; } 
    }
}
