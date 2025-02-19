using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        /// <summary>
        /// Permet de verifier si le nom de département existe deja 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Task<bool> IsNameExistsAsync(string name);
    }
}
