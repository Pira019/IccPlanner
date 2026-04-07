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
}
