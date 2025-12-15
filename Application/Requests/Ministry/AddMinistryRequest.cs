namespace Application.Requests.Ministry
{
    /// <summary>
    /// Modèle pour ajour un <see cref="Ministry"/>
    /// </summary>
    public class AddMinistryRequest
    { 
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
