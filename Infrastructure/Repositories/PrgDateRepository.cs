
using Application.Interfaces.Repositories;
using Application.Responses.Program;
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

        public async Task<GetPrg> GetByMonthYearAsync(int month, int year)
        {
            var query = _dbSet
                  .Where(x => x.Date.HasValue
                              && x.Date.Value.Month == month
                              && x.Date.Value.Year == year);

            // Liste complète des événements
            var events = await query
                .Select(prgDate => new GetEvent
                {
                    Id = prgDate.Id,
                    IdPrg = prgDate.PrgDepartmentInfo.DepartmentProgram.Program.Id,
                    Title = prgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                    IndRecurrent = prgDate.PrgDepartmentInfo.DepartmentProgram.IndRecurent,
                    Date = prgDate.Date!.Value.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            // Liste distincte des programmes
            var programs = events
                .GroupBy(e => new { e.IdPrg, e.Title })       
                .Select(g => new PrgResponse
                {
                    Id = g.Key.IdPrg,       
                    Name = g.Key.Title
                })
                .OrderBy(p => p.Name) 
                .ToList();

            return new GetPrg
            {
                Events = events,
                Prgs = programs
            };
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
