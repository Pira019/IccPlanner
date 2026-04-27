
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface ISendEmailService
    {
        /// <summary>
        ///  Permet d'envoyer un email de confirmation de compte.
        /// </summary>
        /// <param name="user">Nouvel utilisateur créé</param>
        Task SendEmailConfirmation(User user);

        /// <summary>
        ///  Permet d'envoyer un email d'invitation à rejoindre un département.
        /// </summary>
        /// <param name="toEmail">Email du destinataire</param>
        /// <param name="firstName">Prénom de la personne invitée</param>
        /// <param name="departmentName">Nom du département</param>
        /// <param name="invitationCode">Code d'invitation à 4 chiffres</param>
        /// <param name="invitationId">Id de l'invitation</param>
        Task SendInvitationEmail(string toEmail, string firstName, string departmentName, string invitationCode, int invitationId);
    }
}
