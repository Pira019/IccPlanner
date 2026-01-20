using Application.Dtos.Department;
using Application.Dtos.PrgDate;
using Application.Dtos.TabServicePrgDto;
using Application.Interfaces.Repositories;
using Application.Requests.Program;
using Application.Responses.Program;
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
        public async Task<IEnumerable<GetProgramFilterResponse>> GetProgramFilterAsync(GetProgramFilterRequest filter)
        {
            return await _dbSet.Where(d => (filter.DepartmentIds == null
                || d.ProgramDepartments.Any(dp => filter.DepartmentIds.Contains(dp.Department.Id.ToString()))
                )
                && (!filter.Mois.HasValue && !filter.Year.HasValue ||
                d.ProgramDepartments.Any(prgDate => prgDate.PrgDepartmentInfo != null &&
                prgDate.PrgDepartmentInfo.PrgDate.Any(pd => pd.Date.HasValue && pd.Date.Value.Month == filter.Mois && pd.Date.Value.Year == filter.Year))
                ))
                 .Select(d => new GetProgramFilterResponse
                 {
                     Name = d.Name,
                     Departments = d.ProgramDepartments
                        .Select(dp => new DepartmentDto
                        { 
                            Name = dp.Department.Name,
                            ShortName = dp.Department.ShortName
                        }).ToList(),
                     TypeProgram = d.ProgramDepartments.Select(dp => dp.Type)
                        .FirstOrDefault(),

                     Dates = d.ProgramDepartments
                     .Where(dp => dp.PrgDepartmentInfo != null)
                        .SelectMany(dp => dp.PrgDepartmentInfo!.PrgDate
                            .Where(pd =>
                                (!filter.Mois.HasValue && !filter.Year.HasValue ) ||
                                (pd.Date.HasValue && (pd.Date.Value.Month == filter.Mois && pd.Date.Value.Year == filter.Year))
                            )
                            .Select(pd => new PrgDateDto
                            {
                                Date = pd.Date,
                                Department = new DepartmentDto
                                {
                                    Name = dp.Department.Name,
                                    ShortName = dp.Department.ShortName
                                },
                                NbrService = pd.TabServicePrgs.Count(),
                                Services = pd.TabServicePrgs.Select(ts => new ServicePrgDateDto
                                {
                                    Name = ts.TabServices.DisplayName,
                                    Hours = ts.TabServices.StartTime.ToString("hh\\:mm") + " - " + ts.TabServices.EndTime.ToString("hh\\:mm"),
                                    ArrivalHours = ts.ArrivalTimeOfMember.HasValue ? ts.ArrivalTimeOfMember.Value.ToString("hh\\:mm") : null,

                                }).ToList()

                            })).ToList()
                 })
                .ToListAsync();
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await _dbSet.AnyAsync(x => x.Name.ToLower() == name.ToLower()); 
        }
    }
}
