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
    }
}
