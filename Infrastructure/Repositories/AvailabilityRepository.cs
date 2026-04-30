// Ignore Spelling: Prg

using Application.Dtos.AvailabilityDto;
using Application.Interfaces.Repositories;
using Application.Responses.Availability;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AvailabilityRepository : BaseRepository<Availability>, IAvailabilityRepository
    {
        public AvailabilityRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        /// <inheritdoc />
        public Task<GetAvailabityDto?> GetIdByAsync(int tabServicePrgId, Guid userId)
        {
            return _dbSet
                .Where(availability => availability.TabServicePrgId == tabServicePrgId &&
                    availability.DepartmentMember.Member.Id == userId)
                .Select(availability => new GetAvailabityDto
                {
                    Id = availability.Id,
                    DatePrg = availability.TabServicePrg.PrgDate.Date,
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> HasAlreadyChosenAvailability(int servicePrgId, int departmentMemberId)
        {
            return await _dbSet
                 .Where(availability => availability.TabServicePrgId == servicePrgId && availability.DepartmentMemberId == departmentMemberId)
                 .AnyAsync();
        }

        public async Task<List<int>> GetExistingServicePrgIdsAsync(int departmentMemberId, List<int> servicePrgIds)
        {
            return await _dbSet
                .Where(a => a.DepartmentMemberId == departmentMemberId && servicePrgIds.Contains(a.TabServicePrgId))
                .Select(a => a.TabServicePrgId)
                .ToListAsync();
        }

        public async Task<List<UserAvailabilityResponse>> GetUserAvailabilitiesAsync(Guid memberId, int month, int year, int departmentId)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(a => a.DepartmentMember.MemberId == memberId
                    && a.DepartmentMember.DepartmentId == departmentId
                    && a.TabServicePrg.PrgDate.Date.HasValue
                    && a.TabServicePrg.PrgDate.Date.Value.Month == month
                    && a.TabServicePrg.PrgDate.Date.Value.Year == year)
                .GroupBy(a => a.TabServicePrg.PrgDate.Date)
                .Select(g => new UserAvailabilityResponse
                {
                    Date = g.Key,
                    Items = g.OrderBy(a => a.TabServicePrg.TabServices.StartTime)
                        .Select(a => new UserAvailabilityItem
                        {
                            AvailabilityId = a.Id,
                            TabServicePrgId = a.TabServicePrgId,
                            ServiceName = a.TabServicePrg.DisplayName,
                            ProgramName = a.TabServicePrg.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                            StartTime = a.TabServicePrg.TabServices.StartTime.ToString(),
                            EndTime = a.TabServicePrg.TabServices.EndTime.ToString(),
                            ArrivalTime = a.TabServicePrg.ArrivalTimeOfMember.HasValue
                                ? a.TabServicePrg.ArrivalTimeOfMember.ToString()
                                : null
                        }).ToList()
                })
                .OrderBy(r => r.Date)
                .ToListAsync();
        }

        public async Task<List<AvailableMembersByDateResponse>> GetAvailableMembersByDateAsync(int departmentId, DateOnly date)
        {
            var rawData = await _dbSet
                .AsNoTracking()
                .Where(a => a.DepartmentMember.DepartmentId == departmentId
                    && a.TabServicePrg.PrgDate.Date.HasValue
                    && a.TabServicePrg.PrgDate.Date.Value == date)
                .Select(a => new
                {
                    a.Id,
                    a.TabServicePrgId,
                    ServiceName = a.TabServicePrg.DisplayName,
                    ProgramName = a.TabServicePrg.PrgDate.PrgDepartmentInfo.DepartmentProgram.Program.Name,
                    StartTime = a.TabServicePrg.TabServices.StartTime,
                    EndTime = a.TabServicePrg.TabServices.EndTime,
                    ArrivalTime = a.TabServicePrg.ArrivalTimeOfMember,
                    MemberId = a.DepartmentMember.MemberId,
                    MemberName = a.DepartmentMember.Member.Name,
                    MemberLastName = a.DepartmentMember.Member.LastName,
                    IsPlanned = a.Planning != null,
                    PlanningId = a.Planning != null ? (int?)a.Planning.Id : null,
                    PosteId = a.Planning != null ? a.Planning.PosteId : null,
                    IsTraining = a.Planning != null && a.Planning.IndTraining,
                    CreatedAt = a.CreatedAt
                })
                .ToListAsync();

            return rawData
                .GroupBy(a => new { a.TabServicePrgId, a.ServiceName, a.ProgramName, a.StartTime, a.EndTime, a.ArrivalTime })
                .Select(g => new AvailableMembersByDateResponse
                {
                    ServicePrgId = g.Key.TabServicePrgId,
                    ServiceName = g.Key.ServiceName,
                    ProgramName = g.Key.ProgramName,
                    StartTime = g.Key.StartTime.ToString(),
                    EndTime = g.Key.EndTime.ToString(),
                    ArrivalTime = g.Key.ArrivalTime?.ToString(),
                    AvailableMembers = g.OrderBy(a => a.MemberName)
                        .ThenBy(a => a.MemberLastName)
                        .Select(a => new AvailableMemberItem
                        {
                            AvailabilityId = a.Id,
                            PlanningId = a.PlanningId,
                            PosteId = a.PosteId,
                            MemberId = a.MemberId,
                            DisplayName = a.MemberName + " " + a.MemberLastName!.Substring(0, 1) + ".",
                            IsPlanned = a.IsPlanned,
                            IsTraining = a.IsTraining,
                            CreatedAt = a.CreatedAt
                        }).ToList()
                })
                .OrderBy(r => r.StartTime)
                .ToList();
        }
    }
}
