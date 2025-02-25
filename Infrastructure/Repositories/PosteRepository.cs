using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PosteRepository : IPostRepository
    {
        public readonly IccPlannerContext PlannerContext;

        public PosteRepository(IccPlannerContext iccPlannerContext)
        {
            PlannerContext = iccPlannerContext; 
        }
        public async Task<Poste?> FindPosteByName(string name)
        {
            return await PlannerContext.Postes.FirstOrDefaultAsync(p => p.Name == name);   
        }
    }
}
