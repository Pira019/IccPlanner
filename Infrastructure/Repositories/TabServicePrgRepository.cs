// Ignore Spelling: Prg

using System.Security.Cryptography;
using Application;
using Application.Dtos.TabServicePrgDto;
using Application.Interfaces.Repositories;
using Application.Requests.ServiceTab;
using Application.Responses.ServicePrg;
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

        public async Task<GetServiceByDepart?> GetServicePrgByDepart(int idDepart, DateOnly dateOnly, Guid? memberId)
        {
            // Récupère les services groupés par programme
            var servicePrgList = await _dbSet
                .AsNoTracking()
                .Where(s =>
                    s.PrgDate.Date.HasValue &&
                    s.PrgDate.Date.Value == dateOnly &&
                    s.PrgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId == idDepart)
               .GroupBy(s => new { s.PrgDate.PrgDepartmentInfo.DepartmentProgram.ProgramId, s.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name })
                .Select(g => new ServicePrgLst
                {
                    PrgName = g.Key.Name,
                    ServicePrg =
                    g.OrderBy(s => s.ArrivalTimeOfMember ?? s.TabServices.StartTime) // ArrivalTime si existe sinon StartTime
                    .ThenBy(s => s.TabServices.StartTime)
                    .ThenBy(s => s.TabServices.EndTime)
                    .ThenBy(s => s.DisplayName)
                    .Select(s => new ServicePrg
                    {
                        Id = s.Id,
                        IsAvailable = s.Availabilities.Where(a => a.DepartmentMember.MemberId == memberId).Any(),
                        DisplayName = s.DisplayName,
                        Comment = s.Notes,
                        StartTime = s.TabServices.StartTime.ToString(),
                        EndTime = s.TabServices.EndTime.ToString(),
                        ArrivalTime = s.ArrivalTimeOfMember.HasValue
                            ? s.ArrivalTimeOfMember.ToString()!
                            : string.Empty
                    }).ToList()
                })
                .ToListAsync();

            // Crée l'objet final
            if (!servicePrgList.Any())
                return null;

            return new GetServiceByDepart
            {
                Date = dateOnly,
                ServicePrgDates = servicePrgList
            };
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
                             Description = dp.Program.Description,
                             Services = pd.TabServicePrgs.Select(service => new TabServicesPrgDto
                             {
                                 IdTabService = service.Id,
                                 TabServicesId = service.TabServicesId,
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
                            .GroupBy(r => new { r.ProgramId, r.ProgramTitle, r.ShortName, r.Description})
                            .Select(programGroup => new ProgramServiceDto
                            {
                                IdPrg = programGroup.Key.ProgramId,
                                ProgramId = programGroup.Key.ProgramId,
                                ShortName = programGroup.Key.ShortName,
                                Title = programGroup.Key.ProgramTitle,
                                Description = programGroup.Key.Description,
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

        public async Task<bool> AllBelongToDepartmentAsync(List<int> servicePrgIds, int departmentId)
        {
            var count = await _dbSet
                .Where(s => servicePrgIds.Contains(s.Id)
                    && s.PrgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId == departmentId)
                .CountAsync();

            return count == servicePrgIds.Count;
        }

        public async Task<bool> AnyHasPastDateAsync(List<int> servicePrgIds)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _dbSet
                .AnyAsync(s => servicePrgIds.Contains(s.Id)
                    && s.PrgDate.Date.HasValue
                    && s.PrgDate.Date.Value < today);
        }
    }
}
