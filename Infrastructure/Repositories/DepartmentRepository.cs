using System;
using Application.Dtos.Department;
using Application.Dtos.TabServicePrgDto;
using Application.Interfaces.Repositories;
using Application.Responses.Department;
using Application.Responses.TabService;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<DepartmentMember?> FindDepartmentMember(string memberId, int departmentId)
        {
            return await PlannerContext.DepartmentMembers.FirstOrDefaultAsync(x => x.MemberId == Guid.Parse(memberId) && x.DepartmentId == departmentId);
        }

        public async Task<GetDepartResponse> GetDepartAsync(string? membreId, int? pageNumber= null, int? pageSize = null)
        {
            var query = _dbSet.AsNoTracking();

            // Filtrer par membreId si fourni
            if (!string.IsNullOrEmpty(membreId))
            {
                Guid memberGuid = Guid.Parse(membreId);
                query = query.Where(d => d.Members.Any(m => m.Id == memberGuid));
            }

            // Compter le total
            int totalCount = await query.CountAsync();

            // Appliquer le tri
            query = query.OrderBy(d => d.Name);

            // Pagination seulement si les deux sont fournis
            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value)
                             .Take(pageSize.Value);
            }

            // Projection
            var departDtos = await query
                .Select(department => new GetDepartDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    NbrMember = department.Members.Count,
                    NbrProgram = department.Programs.Count,
                    ShortName = department.ShortName,
                })
                .ToListAsync();

            // Retour
            return new GetDepartResponse
            {
                Departments = departDtos,
                PageNumber = pageNumber ?? 1,
                PageSize = pageSize ?? totalCount,
                TotalCount = totalCount
            };
        }

        public Task<IEnumerable<GetServicesListResponse>> GetDepartmentServicesByDate(Guid userId, DateOnly dateOnly)
        {
            throw new NotImplementedException();
        }


        ///inheritdoc />
        /* public async Task<IEnumerable<GetServicesListResponse>> GetDepartmentServicesByDate(Guid userId, DateOnly datePrg)
         {
             return await _dbSet.Where(department => department.Members.Any(member => member.Id == userId))
                 .Select(department => new GetServicesListResponse
                 {
                     DepartmentName = department.Name,
                     ServicePrograms = department.DepartmentPrograms
                    .Where(dp => dp.PrgDepartmentInfo != null)
                    .SelectMany(dp => dp.PrgDepartmentInfo!.PrgDate
                         .Where(pd => pd.Date == datePrg)
                        .SelectMany(pd =>
                         pd.TabServicePrgs.Select(service =>
                         new ServiceProgramDto
                         {
                             ProgramName = dp.Program.Name,
                             ProgramShortName = dp.Program.ShortName,
                             ServiceProgramId = service.Id,
                             DisplayName = service.DisplayName,
                             ServantArrivalTime = service.ArrivalTimeOfMember.ToString(),
                             StartTime = service.TabServices.StartTime,
                             EndTime = service.TabServices.EndTime.ToString(),
                             IsAvailable = service.Availabilities.Any(x => x.DepartmentMember.Member.Id == userId)

                         })))
                             .OrderBy(X => X.StartTime).ToList()
                 }).Where(dpt => dpt.ServicePrograms.Any())
                 .ToListAsync();
         }*/

        public async Task<IEnumerable<int?>> GetValidDepartmentIds(IEnumerable<int> departmentIds)
        {
            // Vérifier si departmentIds est null ou vide
            if (departmentIds == null || !departmentIds.Any())
            {
                return Enumerable.Empty<int?>(); // Retourner une séquence vide si aucun ID n'est fourni
            }

            return await _dbSet.Where(department_ => departmentIds.Contains(department_.Id))
                                                                             .Select(department => (int?)department.Id)
                                                                            .ToListAsync();
        }

        public async Task<bool> IsDepartmentIdExists(int id)
        {
            return await PlannerContext.Departments.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await PlannerContext.Departments.AnyAsync(x => x.Name.ToLower() == name.ToLower());
        }
        public async Task<DepartmentMember> SaveDepartmentMember(DepartmentMember departmentMember)
        {
            await PlannerContext.DepartmentMembers.AddAsync(departmentMember);
            PlannerContext.SaveChanges();
            return departmentMember;
        }

        public async Task SaveDepartmentMemberPost(DepartmentMemberPost departmentMemberPost)
        {
            await PlannerContext.DepartmentMemberPosts.AddAsync(departmentMemberPost);
            PlannerContext.SaveChanges();
        }
    }
}
