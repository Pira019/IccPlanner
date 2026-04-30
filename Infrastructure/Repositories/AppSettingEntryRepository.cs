using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    /// <summary>
    ///     Repository pour les paramètres de l'application.
    /// </summary>
    public class AppSettingEntryRepository : IAppSettingEntryRepository
    {
        private readonly IccPlannerContext _context;

        public AppSettingEntryRepository(IccPlannerContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<AppSettingEntry>> GetByCategoryAsync(string category)
        {
            return await _context.AppSettingEntries
                .AsNoTracking()
                .Where(e => e.Category == category)
                .OrderBy(e => e.Key)
                .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<AppSettingEntry?> GetAsync(string category, string key)
        {
            return await _context.AppSettingEntries
                .FirstOrDefaultAsync(e => e.Category == category && e.Key == key);
        }

        /// <inheritdoc />
        public async Task UpsertAsync(AppSettingEntry entry)
        {
            var existing = await _context.AppSettingEntries
                .FirstOrDefaultAsync(e => e.Category == entry.Category && e.Key == entry.Key);

            if (existing != null)
            {
                existing.Value = entry.Value;
                existing.Unit = entry.Unit;
                existing.DisplayName = entry.DisplayName;
            }
            else
            {
                await _context.AppSettingEntries.AddAsync(entry);
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            await _context.AppSettingEntries.Where(e => e.Id == id).ExecuteDeleteAsync();
        }
    }
}
