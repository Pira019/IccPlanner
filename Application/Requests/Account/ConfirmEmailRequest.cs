﻿
using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Account
{
    /// <summary>
    /// Le parametre pour confirmer l'email
    /// </summary>
    public class ConfirmEmailRequest
    {
        /// <summary>
        /// id de l'utilisateur crée
        /// </summary>
        [Required]
        public required string UserId { get; set; }

        /// <summary>
        /// Le token envoyé
        /// </summary>
        [Required]
        public required string Token { get; set; }
    }
}
