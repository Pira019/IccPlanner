// Ignore Spelling: Prg

using Application.Dtos.AvailabilityDto;
using Application.Interfaces.Repositories;
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
    }
}
