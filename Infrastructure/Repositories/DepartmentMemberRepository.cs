using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence; 

namespace Infrastructure.Repositories
{
    public class DepartmentMemberRepository : BaseRepository<DepartmentMember>, IDepartmentMemberRepository
    {
        public DepartmentMemberRepository(IccPlannerContext plannerContext) : base(plannerContext)
        { }  
    }
}
