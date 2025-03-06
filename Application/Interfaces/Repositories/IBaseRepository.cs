﻿namespace Application.Interfaces.Repositories
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
        public Task BulkInsertOptimizedAsync(IEnumerable<T> entities);
    }
}
