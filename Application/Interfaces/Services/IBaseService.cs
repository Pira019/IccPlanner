namespace Application.Interfaces.Services
{
    /// <summary>
    ///      Permet de gérer des fonction CRUD
    /// </summary>
    public interface IBaseService<T> where T : class
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
        public Task<Result<T>> Add (T requestBody); 
        
        /// <summary>
        ///     Obtenir tous les objets enregistres 
        /// </summary>
        /// <returns>
        ///    Un objet Response(DTO)
        /// </returns>
        public Task<IEnumerable<T>> GetAll ();

        /// <summary>
        ///  Supprimer un objet par son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteByIdAsync(int id);
        public Task DeleteSoftByIdAsync(int id); 
    }
} 