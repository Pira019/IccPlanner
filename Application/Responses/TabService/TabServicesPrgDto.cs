namespace Application.Responses.TabService
{
    /// <summary>
    ///     Represente un service associe a une date de programme.
    /// </summary>
    public class TabServicesPrgDto
    {
        /// <summary>
        ///     Identifiant du TabServicePrg.
        /// </summary>
        public int IdTabService { get; set; }

        /// <summary>
        ///     Identifiant du service de base (TabServices).
        /// </summary>
        public int TabServicesId { get; set; }

        /// <summary>
        ///     Nom d'affichage du service.
        /// </summary>
        public string ServiceTitle { get; set; } = string.Empty;

        /// <summary>
        ///     Heure de debut du service.
        /// </summary>
        public TimeOnly StartTime { get; set; }

        /// <summary>
        ///     Heure de fin du service.
        /// </summary>
        public TimeOnly EndTime { get; set; }

        /// <summary>
        ///     Heure d'arrivee des membres (nullable).
        /// </summary>
        public TimeOnly? ArrivalTime { get; set; }

        /// <summary>
        ///     Notes ou commentaires sur le service.
        /// </summary>
        public string? Notes { get; set; }
    }
}
