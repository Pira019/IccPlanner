namespace Application.Interfaces.Repositories
{
    /// <summary>
    /// Cette interface permet de regrouper les fonctionnalites CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T : class
    {
        //  public Task insert(T entity); 
        Task<IEnumerable<T>> GetAllAsync();
    }
}
