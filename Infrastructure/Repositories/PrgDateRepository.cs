
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

        // <inheritdoc />
        public async Task<IEnumerable<DateOnly>> GetPrgDates(Guid userId, DateOnly dateFilter)
        {
            return await _dbSet
                .Where(x => x.PrgDepartmentInfo.DepartmentProgram.Department.Members.Any(m => m.Id == userId)
                        && x.Date.HasValue
                        && x.Date.Value.Month == dateFilter.Month && x.Date.Value.Year == dateFilter.Year
                        )
                .Select(prgDate => prgDate.Date)
                .Select(date => date.Value)
                .Distinct()
                .ToListAsync();
        }

        // <inheritdoc />
        public async Task<IEnumerable<int>> GetRecurringPrgDateIdsFromNowAsync(int prgDateId)
        {
            return await _dbSet.Include(x => x.PrgDepartmentInfo)
                                        .ThenInclude(p => p.DepartmentProgram)
                                        .Where(x => x.Id == prgDateId
                                                  && x.PrgDepartmentInfo.DepartmentProgram.IndRecurent
                                                  && x.Date >= DateOnly.FromDateTime(DateTime.Now))
                                        .Select(x => x.Id)
                                        .ToArrayAsync();
        }
    }
}
