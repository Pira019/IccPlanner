using System.Xml.Linq;
using Application.Interfaces.Repositories;
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
            return await PlannerContext.DepartmentMembers.FirstOrDefaultAsync(x => x.MemberId == Guid.Parse(memberId) && x.DepartementId == departmentId);
        }

        public async Task<bool> IsDepartmentIdExists(int id)
        {
            return await PlannerContext.Departments.AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await PlannerContext.Departments.AnyAsync(x => x.Name == name);
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
