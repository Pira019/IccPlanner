
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;

namespace Infrastructure.Repositories
{
    public class PrgDateRepository : BaseRepository<PrgDate>, IPrgDateRepository
    {
        public PrgDateRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<IEnumerable<int>> GetRecurringPrgDateIdsFromNowAsync(int prgDateId)
        {
            return await _dbSet.Include(x => x.PrgDepartmentInfo)
                                        .ThenInclude(p => p.DepartmentProgram)
                                        .Where(x => x.Id == prgDateId
                                                  && x.PrgDepartmentInfo.DepartmentProgram.Type == ProgramType.Recurring.ToString()
                                                  && x.Date >= DateOnly.FromDateTime(DateTime.Now))
                                        .Select(x => x.Id)
                                        .ToArrayAsync();
        }
    }
}
