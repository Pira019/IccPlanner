 
namespace Application.Interfaces
{
    /// <summary>
    /// Cette interface permet de regrouper les fonctionnalites CRUD
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T>
    {
        void insert(T entity);
    }
}
