using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProgramRepository : BaseRepository<Program>, IProgramRepository
    {
        public ProgramRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {           
            return await _dbSet.AnyAsync(x => EF.Functions.ILike(x.Name,name));
        }
    }
}
