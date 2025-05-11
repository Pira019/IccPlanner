namespace Application.Interfaces.Services
{
    /// <summary>
    ///      Permet de gérer des fonction CRUD
    /// </summary>
    public interface IBaseService
    {
        /// <summary>
        ///     Permet de persister un objet
        /// </summary>
        /// <param name="requestBody">
        ///     Définit la classe passée dans le contrôleur
        /// </param>
        /// <returns>
        ///     Définit une classe Response
        /// </returns>
        public Task<Object> Add (Object requestBody);  
    }
} 