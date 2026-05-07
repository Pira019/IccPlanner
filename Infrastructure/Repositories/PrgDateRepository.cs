
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
                    DepartmentId = prgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId,
                    DepartmentName = prgDate.PrgDepartmentInfo.DepartmentProgram.Department.Name,
                    DepartmentShortName = prgDate.PrgDepartmentInfo.DepartmentProgram.Department.ShortName,
                    Title = prgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                    ShortName = prgDate.PrgDepartmentInfo.DepartmentProgram.Program.ShortName,
                    IndRecurrent = prgDate.PrgDepartmentInfo.DepartmentProgram.IndRecurent,
                    Date = prgDate.Date!.Value.ToString("yyyy-MM-dd")
                })
                .ToListAsync();

            // Liste distincte des programmes
            var programs = events
                .GroupBy(e => new { e.IdPrg, e.Title, e.ShortName })       
                .Select(g => new PrgResponse
                {
                    Id = g.Key.IdPrg,       
                    Name = g.Key.Title,
                    ShortName = g.Key.ShortName
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
        public async Task<IEnumerable<DateOnly>> GetPrgDatesAsync(Guid userId, DateOnly dateFilter)
        {
            return await _dbSet
                .Where(x => x.PrgDepartmentInfo.DepartmentProgram.Department.Members.Any(m => m.Id == userId)
                        && x.Date.HasValue
                        && x.Date.Value.Month == dateFilter.Month && x.Date.Value.Year == dateFilter.Year
                        )
                .Select(prgDate => prgDate.Date)
                .Select(date => date!.Value)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<DateOnly>> GetPrgServiceDatesAsync( int month, int year, int idDepart)
        {
            var startDate = new DateOnly(year, month, 1);
            var endDate = startDate.AddMonths(1);

           return await _dbSet
                .Where(x => x.PrgDepartmentInfo.DepartmentProgram.DepartmentId == idDepart
                        && x.Date.HasValue
                        && x.Date.Value >= startDate && x.Date.Value < endDate
                        && x.TabServicePrgs.Any()
                        )
                .Select(prgDate => prgDate.Date!.Value) 
                .Distinct()
                .ToListAsync();
        }

        // <inheritdoc />
        public async Task<IEnumerable<int>> GetRecurringPrgDateIdsFromNowAsync(int prgDateId)
        {
            // Trouver le PrgDate et son programme
            var prgDate = await _dbSet
                .Include(x => x.PrgDepartmentInfo)
                    .ThenInclude(x => x.DepartmentProgram)
                .FirstOrDefaultAsync(x => x.Id == prgDateId);

            if (prgDate == null || prgDate.PrgDepartmentInfo?.DepartmentProgram == null || !prgDate.PrgDepartmentInfo.DepartmentProgram.IndRecurent)
                return Array.Empty<int>();

            var programId = prgDate.PrgDepartmentInfo.DepartmentProgram.ProgramId;

            // Retourner toutes les PrgDate de TOUS les departements qui ont ce programme, a partir d'aujourd'hui
            return await _dbSet
                .Where(x => x.PrgDepartmentInfo.DepartmentProgram.ProgramId == programId
                          && x.PrgDepartmentInfo.DepartmentProgram.IndRecurent
                          && x.Date.HasValue
                          && x.Date.Value >= DateOnly.FromDateTime(DateTime.UtcNow))
                .Select(x => x.Id)
                .ToArrayAsync();
        }
    }
}
