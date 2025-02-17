using Application.Interfaces.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Permet de factoriser les operations communes
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly IccPlannerContext PlannerContext;

        public BaseRepository(IccPlannerContext plannerContext)
        {
            this.PlannerContext = plannerContext;
        }
        public async Task<TEntity> Insert(TEntity entity)
        {
            await PlannerContext.Set<TEntity>().AddAsync(entity);
            await PlannerContext.SaveChangesAsync();
            return entity;
        }
    }
}
