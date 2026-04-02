using Application.Interfaces.Repositories;
using Application.Responses.Department;
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

        public async Task<Poste> InsertAsync(Poste poste)
        {
            await PlannerContext.Postes.AddAsync(poste);
            await PlannerContext.SaveChangesAsync();
            return poste;
        }

        public async Task<Poste?> GetByIdAsync(int id)
        {
            return await PlannerContext.Postes.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<PosteResponse>> GetAllAsync()
        {
            return await PlannerContext.Postes
                .AsNoTracking()
                .Select(p => new PosteResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    ShortName = p.ShortName
                })
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task UpdateAsync(Poste poste)
        {
            await PlannerContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await PlannerContext.Postes.Where(p => p.Id == id).ExecuteDeleteAsync();
        }

        public async Task<bool> IsNameExistsAsync(string name)
        {
            return await PlannerContext.Postes.AnyAsync(p => p.Name.ToLower() == name.ToLower());
        }
    }
}
