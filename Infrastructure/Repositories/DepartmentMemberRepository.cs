using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class DepartmentMemberRepository : BaseRepository<DepartmentMember>, IDepartmentMemberRepository
    {
        public DepartmentMemberRepository(IccPlannerContext plannerContext) : base(plannerContext)
        { }

        public Task<bool> CanManageMembersAsync(string userId)
        {
            return _dbSet.AnyAsync(dm =>
                 dm.Member != null &&
                 dm.Member.User != null &&
                 dm.Member.User.Id == userId &&
                 dm.DepartmentMemberPosts.Any(dr => dr.Poste.IndGest)
             );
        } 

        public async Task<int?> GetMemberInDepartmentIdAsync(int? departmentId, Guid memberId)
        {
            return await _dbSet
                 .Where(dm => dm.DepartmentId == departmentId && dm.MemberId == memberId)
                 .Select(dm => (int?)dm.Id)
                 .FirstOrDefaultAsync();
        }

        public async Task<bool> HasManagementRightAsync(Guid memberId, int departmentId)
        {
            return await _dbSet.AnyAsync(dm =>
                dm.MemberId == memberId
                && dm.DepartmentId == departmentId
                && dm.DepartmentMemberPosts.Any(dmp => dmp.Poste.IndGest));
        }

        public async Task<bool> HasPlanningRightAsync(Guid memberId, int departmentId)
        {
            return await _dbSet.AnyAsync(dm =>
                dm.MemberId == memberId
                && dm.DepartmentId == departmentId
                && dm.IndPlanning);
        }
    }
}
