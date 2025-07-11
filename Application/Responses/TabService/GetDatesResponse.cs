namespace Application.Responses.TabService
{
    /// <summary>
    ///     Modèle de réponse pour la récupération des dates.
    /// </summary>
    public class GetDatesResponse
    {
        /// <summary>
        ///     Liste des dates disponibles.
        /// </summary>
        public IEnumerable<DateOnly>? Dates { get; set; }
    }
}
