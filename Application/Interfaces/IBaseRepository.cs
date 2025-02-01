 
namespace Application.Interfaces
{
    /// <summary>
    /// Cette interface permet de regrouper les fonctionnalites CRUD
    /// </summary>
    /// <typeparam name="Model">Represente la classe</typeparam>
    public interface IBaseRepository<Model>
    {
        public Task insert(Model entity);
    }
}
