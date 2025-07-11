namespace Application.Dtos.TabServicePrgDto
{
    /// <summary>
    ///     Représente un DTO pour les dates de programme de service.
    /// </summary>
    public class ServicePrgDateDto
    {
        public string? Name { get; set; }

        /// <summary>
        ///     Représente l'heure du debut et de la fin.
        /// </summary>
        public string? Hours { get; set; }

        /// <summary>
        ///     Représente l'heure d'arrivée au service.
        /// </summary>
        public string? ArrivalHours { get; set; }
    }
}
