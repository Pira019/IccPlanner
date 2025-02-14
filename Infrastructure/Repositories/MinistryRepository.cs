using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MinistryRepository : BaseRepository<Ministry>, IMinistryRepository
    {
        public MinistryRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        } 
        public async Task<bool> IsNameExists(string name)
        {
            return await PlannerContext.Ministries.AnyAsync(x => x.Name == name);
        }
    }
}
