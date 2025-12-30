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

        public async Task BulkDeleteByIdsAsync(IEnumerable<int> ids)
        {
            //await _dbSet.Where(e => ids.Contains(EF.Property<int>(e, "Id"))).ExecuteDelete();
        }

        public async Task InsertAllAsync(IEnumerable<TEntity> entities)
        {
            foreach (var chuck in entities.Chunk(1000))
            {
                await _dbSet.AddRangeAsync(chuck);
                await PlannerContext.SaveChangesAsync();
            }
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

        public IQueryable<TEntity> QueryAll()
        {
            return _dbSet.AsQueryable();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbSet.Where(x => EF.Property<int>(x, "Id") == id).ExecuteDeleteAsync();
        }

        public async Task<TEntity?> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => EF.Property<int>(x, "Id") == id);
        }

        public async Task UpdateAsync(TEntity entity, TEntity existingEntity)
        {            
            // Copier les valeurs de la nouvelle entité vers l'existante
            PlannerContext.Entry(existingEntity).CurrentValues.SetValues(entity);

            await PlannerContext.SaveChangesAsync();
        }

        public async Task DeleteSoftAsync(int id)
        {
            await _dbSet
                    .Where(x => EF.Property<int>(x, "Id") == id)
                    .ExecuteUpdateAsync(s => s
                        .SetProperty(e => EF.Property<bool>(e, "IsDeleted"), true)
                        .SetProperty(e => EF.Property<DateTimeOffset?>(e, "DeletedAt"), DateTimeOffset.UtcNow)
                    );
        }
    }
}
