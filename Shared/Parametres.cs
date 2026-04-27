namespace Shared
{
    /// <summary>
    ///     Paramètres de l'application.
    /// </summary>
    public class Parametres
    {
        public int DureeInvitationEnMin { get; set; }

        /// <summary>
        ///     Nombre de jours à l'avance pour générer les dates récurrentes (valeur par défaut).
        /// </summary>
        public int RecurrentDaysAhead { get; set; } = 30;
    }

    /// <summary>
    ///     Configuration Brevo (API) pour l'envoi d'emails.
    /// </summary>
    public class BrevoSettings
    {
        /// <summary>
        ///     Clé API Brevo (v3).
        /// </summary>
        public string ApiKey { get; set; } = string.Empty;

        /// <summary>
        ///     Adresse email de l'expéditeur.
        /// </summary>
        public string FromEmail { get; set; } = string.Empty;

        /// <summary>
        ///     Nom affiché de l'expéditeur.
        /// </summary>
        public string FromName { get; set; } = string.Empty;
    }
}
