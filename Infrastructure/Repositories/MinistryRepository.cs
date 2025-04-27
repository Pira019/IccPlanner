using Application.Interfaces.Repositories;
using Application.Responses.Ministry;
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

        public async Task<IEnumerable<GetMinistriesResponse>> GetAll()
        {
            return await _dbSet.Select(ministry => new GetMinistriesResponse { Id = ministry.Id, Name = ministry.Name }).OrderBy(x=> x.Name).ToListAsync();
        }

        public async Task<bool> IsExists(int id)
        {
           return await PlannerContext.Ministries.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsNameExists(string name)
        {
            return await PlannerContext.Ministries.AnyAsync(x => x.Name == name);
        }
    }
}
