using Application.Interfaces.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Permet de factoriser les operations communes
    /// </summary>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public readonly IccPlannerContext PlannerContext;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IccPlannerContext plannerContext)
        {
            this.PlannerContext = plannerContext;
            _dbSet = PlannerContext.Set<TEntity>();
        }

        public async Task BulkInsertOptimizedAsync(IEnumerable<TEntity> entities)
        {
            await PlannerContext.BulkInsertOptimizedAsync(entities);
        }

        public async Task<TEntity> Insert(TEntity entity)
        {
            await PlannerContext.Set<TEntity>().AddAsync(entity);
            await PlannerContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> IsExist(object id)
        {
            // Convertir le paramètre id en chaîne ou entier si possible
            var idValue = id switch
            {
                int intId => (object)intId,
                string stringId => (object)stringId,
                _ => null // Si ce n'est ni un int ni un string, retourne null
            };

            if (idValue == null) return false;

            return await _dbSet.AnyAsync(x => EF.Property<object>(x, "Id") == id);
        }
    }
}
