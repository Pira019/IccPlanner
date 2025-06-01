// Ignore Spelling: Prg

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

        public async Task<bool> HasAlreadyChosenAvailability(int servicePrgId, int departmentMemberId)
        {
            return await _dbSet
                 .Where(availability => availability.TabServicePrgId == servicePrgId && availability.DepartmentMemberId == departmentMemberId)
                 .AnyAsync();
        }
    }
}
