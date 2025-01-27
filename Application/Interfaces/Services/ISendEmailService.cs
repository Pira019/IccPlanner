
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISendEmailService
    {
        /// <summary>
        ///  Permet d'envoyer un email
        /// </summary>
        /// <param name="user">Nouvel utilisateur creer</param>
        Task SendEmailConfirmation(User user); 
    }
}
