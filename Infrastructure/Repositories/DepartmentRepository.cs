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

        public async Task<List<DepartmentServicesResponse>> GetDepartmentServicesByDateAsync(DateOnly datePrg)
        {
            var rawData = await PlannerContext.TabServicePrgs
                .AsNoTracking()
                .Where(tsp => tsp.PrgDate.Date.HasValue && tsp.PrgDate.Date.Value == datePrg)
                .Select(tsp => new
                {
                    DepartmentId = tsp.PrgDate.PrgDepartmentInfo.DepartmentProgram.DepartmentId,
                    DepartmentName = tsp.PrgDate.PrgDepartmentInfo.DepartmentProgram.Department.Name,
                    DepartmentShortName = tsp.PrgDate.PrgDepartmentInfo.DepartmentProgram.Department.ShortName,
                    ServicePrgId = tsp.Id,
                    ServiceName = tsp.DisplayName,
                    ProgramName = tsp.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                    StartTime = tsp.TabServices.StartTime,
                    EndTime = tsp.TabServices.EndTime,
                    ArrivalTime = tsp.ArrivalTimeOfMember
                })
                .ToListAsync();

            return rawData
                .GroupBy(r => new { r.DepartmentId, r.DepartmentName, r.DepartmentShortName })
                .Select(g => new DepartmentServicesResponse
                {
                    DepartmentId = g.Key.DepartmentId,
                    DepartmentName = g.Key.DepartmentName,
                    DepartmentShortName = g.Key.DepartmentShortName,
                    Services = g.OrderBy(s => s.StartTime)
                        .Select(s => new DepartmentServiceItem
                        {
                            ServicePrgId = s.ServicePrgId,
                            ServiceName = s.ServiceName,
                            ProgramName = s.ProgramName,
                            StartTime = s.StartTime.ToString(),
                            EndTime = s.EndTime.ToString(),
                            ArrivalTime = s.ArrivalTime?.ToString()
                        }).ToList()
                })
                .OrderBy(d => d.DepartmentName)
                .ToList();
        }


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

        public async Task<List<PosteResponse>> GetPostesByDepartmentAsync(int departmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(d => d.Id == departmentId)
                .SelectMany(d => d.Postes)
                .Select(p => new PosteResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortName = p.ShortName
                })
                .ToListAsync();
        }

        public async Task AssignPostesAsync(int departmentId, List<int> posteIds)
        {
            var department = await _dbSet
                .Include(d => d.Postes)
                .FirstOrDefaultAsync(d => d.Id == departmentId);

            if (department == null) return;

            var postesToAdd = await PlannerContext.Postes
                .Where(p => posteIds.Contains(p.Id))
                .ToListAsync();

            // Ajouter seulement ceux qui ne sont pas déjà affectés
            foreach (var poste in postesToAdd)
            {
                if (!department.Postes.Any(p => p.Id == poste.Id))
                    department.Postes.Add(poste);
            }

            await PlannerContext.SaveChangesAsync();
        }
    }
}
