// Ignore Spelling: Prg

using System.Security.Cryptography;
using Application.Dtos.TabServicePrgDto;
using Application.Interfaces.Repositories;
using Application.Requests.ServiceTab;
using Application.Responses.TabService;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TabServicePrgRepository : BaseRepository<TabServicePrg>, ITabServicePrgRepository
    {
        public TabServicePrgRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<int?> GetDepartmentIdByServicePrgId(int servicePrgId)
        {
            return await _dbSet
                 .Include(service => service.PrgDate)
                  .ThenInclude(service => service.PrgDepartmentInfo)
                 .Where(service => service.Id == servicePrgId)
                 .Select(service => service.PrgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId)
                 .FirstOrDefaultAsync();
        }

        public async Task<List<GetServicesListResponse>> GetServicesAsync(ServicesRequest request)
        {
            var query = PlannerContext.DepartmentPrograms
                .AsNoTracking()
                .AsQueryable();

            if (request.DepartmentIds != null && request.DepartmentIds.Any())
            {
                query = query.Where(dp => request.DepartmentIds.Contains(dp.DepartmentId));
            }

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(dp => dp.Program.Name.Contains(request.Title));
            }

            if (request.IndRecureent.HasValue)
            {
                query = query.Where(dp => dp.IndRecurent == request.IndRecureent);
            }

            // Récupérer les données brutes
            var rawData = await query
                 .SelectMany(dp => dp.PrgDepartmentInfos!
                     .SelectMany(pdi => pdi.PrgDate  // 👈 SelectMany ici pour avoir une ligne par Date
                         .Where(pd =>
                             pd.Date.HasValue &&
                             (!request.month.HasValue || pd.Date.Value.Month == request.month) &&
                             (!request.year.HasValue || pd.Date.Value.Year == request.year)
                         )
                         .Select(pd => new
                         {
                             PrgDateId = pd.Id,
                             Day = pdi.Day,
                             pd.Date,             
                             dp.IndRecurent,
                             ProgramId = dp.ProgramId,
                             ProgramTitle = dp.Program.Name,
                             ShortName = dp.Program.ShortName,
                             Services = pd.TabServicePrgs.Select(service => new TabServicesPrgDto
                             {
                                 IdTabService = service.Id,
                                 ServiceTitle = service.DisplayName,
                                 StartTime = service.TabServices.StartTime,
                                 EndTime = service.TabServices.EndTime,
                                 ArrivalTime = service.ArrivalTimeOfMember
                             }).ToList()
                         })
                     ))
                 .ToListAsync();

                var result = rawData
                    .GroupBy(r => new
                    {
                        GroupKey = r.IndRecurent
                        ? r.Day
                        : r.Date!.Value.ToString("yyyy-MM-dd")
                    })
                    .Select(group => new GetServicesListResponse
                    {
                        GroupKey = group.Key.GroupKey!,
                        ServicePrograms = group
                            .GroupBy(r => new { r.ProgramId, r.ProgramTitle, r.ShortName})
                            .Select(programGroup => new ProgramServiceDto
                            {
                                IdPrg = programGroup.Key.ProgramId,
                                ShortName = programGroup.Key.ShortName,
                                Title = programGroup.Key.ProgramTitle,
                                Services = programGroup
                                    .SelectMany(r => r.Services)
                                    .ToList()
                            })
                            .ToList()
                    })
                   .OrderByDescending(r => DateTime.TryParse(r.GroupKey, out var date)
                                                                ? date.Ticks
                                                                : long.Parse(r.GroupKey) * -1
                                                                )
                    .ToList();

            return result;
        }

        public Task<bool> IsServicePrgExistAsync(int tabServiceId, int prgDateId)
        {
            return _dbSet.Where(service => service.TabServicesId == tabServiceId && service.PrgDateId == prgDateId).AnyAsync();
        }
    }
}
