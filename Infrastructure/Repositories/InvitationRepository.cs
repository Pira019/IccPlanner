
using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Invitation?> FindValidInv(int invitationId)
        {
           return await _dbSet.FirstOrDefaultAsync(i => i.Id == invitationId
                                                    &&  i.DateExpiration > DateTime.UtcNow 
                                                    && !i.IndUsed);
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

        /// <inheritdoc />
        public async Task<List<Invitation>> FindByEmailsAsync(List<string> emails)
        {
            var lowerEmails = emails.Select(e => e.ToLower()).ToList();
            return await _dbSet
                .Where(i => lowerEmails.Contains(i.Email.ToLower()))
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task InsertAllAsync(List<Invitation> invitations)
        {
            await _dbSet.AddRangeAsync(invitations);
            await PlannerContext.SaveChangesAsync();
        }
    }
}
