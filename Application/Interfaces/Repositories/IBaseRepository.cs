namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Cette interface permet de regrouper les fonctionnalites CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {
        //  public Task insert(T entity); 
        Task<IEnumerable<T>> GetAllAsync();
    }
}
