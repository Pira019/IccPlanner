using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories
{
    public class PrgDepartmentInfoRepository : BaseRepository<PrgDepartmentInfo>, IPrgDepartmentInfoRepository
    {
        public PrgDepartmentInfoRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }
    }
}
