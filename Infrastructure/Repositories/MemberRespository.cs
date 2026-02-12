using Application.Interfaces.Repositories;
using Application.Responses.Member;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MemberRespository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRespository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public async Task<MembersResponse> GetByDepartmentIdAsync(int departmentId)
        {
           var members = await PlannerContext.DepartmentMembers.AsNoTracking()
                 .Where(dm => dm.DepartmentId == departmentId)
                 .Select(dm => new MemberItem
                 {
                     IdDepartMember = dm.Id,
                     Name = dm.Member.Name + " " + dm.Member.LastName,
                     NickName = dm.NickName,
                     Sex = dm.Member.Sexe,
                     Status = dm.Status.ToString(),
                     Postes = dm.DepartmentMemberPosts.Select(p => p.Poste.Name)
                     .ToList()
                 }).ToListAsync(); 

            return new MembersResponse
            {
                Members = members
            };
        }
    }
}
