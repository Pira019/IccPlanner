namespace Application.Interfaces.Services
{
    public interface IRecurrentDateService
    {
        /// <summary>
        ///     Génère les dates récurrentes manquantes pour tous les programmes récurrents actifs.
        /// </summary>
        /// <param name="defaultDaysAhead">Nombre de jours par défaut si le département n'a pas de valeur</param>
        /// <returns>Nombre de dates créées</returns>
        public Task<int> GenerateRecurrentDatesAsync(int defaultDaysAhead);
    }
}
