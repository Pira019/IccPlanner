
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;

namespace Infrastructure.Repositories
{
    public class InvitationRepository : BaseRepository<Invitation>, IInvitationRepository
    {
        public InvitationRepository(IccPlannerContext plannerContext) : base(plannerContext)
        {
        }

        public Task<Invitation?> FindByEmail(string email)
        {
            return _dbSet.FirstOrDefaultAsync(invitation => invitation.Email.ToLower() == email.ToLower());
        }

        public async Task<bool> IsEmailUsedAsync(string email)
        {
            return await _dbSet.AnyAsync(invitation => invitation.Email.ToLower() == email.ToLower() 
                                                        && invitation.IndUsed); 
        }

        public async Task MarkAsUsedAsync(int invitationId)
        {
            var invitation = await _dbSet.FirstOrDefaultAsync(i => i.Id == invitationId);

            invitation!.IndUsed = true;
            invitation.DateUsed = DateTime.UtcNow;
            invitation.IndAct = false;

            _dbSet.Update(invitation);
            await PlannerContext.SaveChangesAsync();

        }
    }
}
