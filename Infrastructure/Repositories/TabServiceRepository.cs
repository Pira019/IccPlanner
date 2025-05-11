using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TabServiceRepository : BaseRepository<TabServices>, IServiceRepository
    {
        public TabServiceRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<bool> IsServiceExist(TimeOnly startTime, TimeOnly endTime, string displayServiceName)
        {
            return await _dbSet.AnyAsync(x => x.StartTime == startTime && x.EndTime == endTime && x.DisplayName.ToLower() == displayServiceName.ToLower());
        }
    }
}
