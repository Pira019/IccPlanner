namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Cette interface permet de regrouper les fonctionnalités CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Ajouter une entity
        /// </summary>
        /// <param name="entity">Prend un modèle générique </param>
        /// <returns><see cref="Task"/> représente l'opération asynchrone, 
        /// contenant <see cref="T"/> de l'opération </returns>
        public Task<T> Insert(T entity);

        /// <summary>
        /// Flag si un model exist par son Id
        /// </summary>
        /// <param name="id">Id du model</param>
        /// <returns>Return <see cref="Task"/> qui représente l'opération asynchrone de type <see cref="bool"/>
        /// true si il existe un entité et false si non</returns>
        public Task<bool> IsExist(object id);

        /// <summary>
        /// Exécuter une opération en masse pour créer plusieurs entités 
        /// </summary>
        /// <param name="entities"></param>
        /// /// <returns>Un <see cref="Task"/> qui représente l'opération asynchrone </returns>
        public Task InsertAllAsync(IEnumerable<T> entities);

        /// <summary>
        /// Permet de supprimer par id
        /// </summary>
        /// <param name="ids">List des Ids a supprimer</param>
        /// <returns></returns>
        public Task BulkDeleteByIdsAsync(IEnumerable<int> ids);

        /// <summary>
        /// Récupère toutes les entités de type <typeparamref name="T"/> depuis la source de données.
        /// </summary>
        /// <returns>
        /// Une collection contenant toutes les entités de type <typeparamref name="T"/>.
        /// </returns>
        public IQueryable<T> QueryAll();

        /// <summary>
        ///     Obtenir toute la liste
        /// </summary>
        /// <returns>
        ///     Une liste des elements 
        /// </returns>
        public Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        ///     Supprimer une entité par son identifiant.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
