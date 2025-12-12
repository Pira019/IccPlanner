namespace Application.Requests.Ministry
{
    /// <summary>
    /// Modèle pour ajour un <see cref="Ministry"/>
    /// </summary>
    public class AddMinistryRequest
    {
        /// <summary>
        ///     Identifiant du ministère (utilisé pour la mise à jour).
        /// </summary>
        public int Id { get; set; } = default;

        /// <summary>
        ///     Nom du ministère.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        ///     Description du ministère.
        /// </summary>
        public required string Description { get; set; } 
    }
}
