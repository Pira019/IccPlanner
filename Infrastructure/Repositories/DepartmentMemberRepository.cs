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

        /// <inheritdoc />
        public async Task<List<(string Email, string Name)>> GetAutoPlanningRecipientsAsync(int departmentId)
        {
            var data = await PlannerContext.DepartmentMemberPosts
                .AsNoTracking()
                .Where(dmp => dmp.DepartmentMember.DepartmentId == departmentId
                    && dmp.IndAutoPlanning
                    && dmp.DepartmentMember.Member.User != null
                    && dmp.DepartmentMember.Member.User.Email != null)
                .Select(dmp => new
                {
                    Email = dmp.DepartmentMember.Member.User!.Email!,
                    Name = dmp.DepartmentMember.Member.Name
                })
                .Distinct()
                .ToListAsync();

            return data.Select(x => (x.Email, x.Name)).ToList();
        }
    }
}
