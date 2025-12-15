namespace Application.Requests.Ministry
{
    /// <summary>
    /// Modèle pour ajour un <see cref="Ministry"/>
    /// </summary>
    public class EditMinistryRequest : AddMinistryRequest
    {
        /// <summary>
        ///     Identifiant du ministère (utilisé pour la mise à jour).
        /// </summary>
        public int Id { get; set; } 
    }
}
