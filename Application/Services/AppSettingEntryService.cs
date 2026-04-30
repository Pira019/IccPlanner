using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Responses.Settings;
using Domain.Entities;

namespace Application.Services
{
    /// <summary>
    ///     Service pour gérer les paramètres de l'application.
    /// </summary>
    public class AppSettingEntryService : IAppSettingEntryService
    {
        private readonly IAppSettingEntryRepository _repository;
        private const string CATEGORY = "deadline";

        public AppSettingEntryService(IAppSettingEntryRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public async Task<DeadlineSettingsResponse> GetDeadlineSettingsAsync()
        {
            var entries = await _repository.GetByCategoryAsync(CATEGORY);
            var response = new DeadlineSettingsResponse();

            var global = entries.FirstOrDefault(e => e.Key == "global");
            if (global != null)
            {
                int.TryParse(global.Value, out var val);
                response.GlobalDeadline = val;
                response.GlobalUnit = global.Unit ?? "days";
            }
            else
            {
                // Valeur par défaut si pas encore en base
                response.GlobalDeadline = 3;
                response.GlobalUnit = "days";
            }

            response.ProgramRules = entries
                .Where(e => e.Key.StartsWith("program:"))
                .Select(e => new DeadlineRuleResponse
                {
                    Id = e.Id,
                    Name = e.DisplayName ?? e.Key,
                    Deadline = int.TryParse(e.Value, out var v) ? v : 0,
                    Unit = e.Unit ?? "days"
                }).ToList();

            response.DepartmentRules = entries
                .Where(e => e.Key.StartsWith("department:"))
                .Select(e => new DeadlineRuleResponse
                {
                    Id = e.Id,
                    Name = e.DisplayName ?? e.Key,
                    Deadline = int.TryParse(e.Value, out var v) ? v : 0,
                    Unit = e.Unit ?? "days"
                }).ToList();

            return response;
        }

        /// <inheritdoc />
        public async Task SaveDeadlineSettingsAsync(SaveDeadlineSettingsRequest request)
        {
            // Global
            await _repository.UpsertAsync(new AppSettingEntry
            {
                Category = CATEGORY,
                Key = "global",
                Value = request.GlobalDeadline.ToString(),
                Unit = request.GlobalUnit
            });

            // Programme rules
            foreach (var rule in request.ProgramRules)
            {
                await _repository.UpsertAsync(new AppSettingEntry
                {
                    Category = CATEGORY,
                    Key = $"program:{rule.Name}",
                    Value = rule.Deadline.ToString(),
                    Unit = rule.Unit,
                    DisplayName = rule.Name
                });
            }

            // Department rules
            foreach (var rule in request.DepartmentRules)
            {
                await _repository.UpsertAsync(new AppSettingEntry
                {
                    Category = CATEGORY,
                    Key = $"department:{rule.Name}",
                    Value = rule.Deadline.ToString(),
                    Unit = rule.Unit,
                    DisplayName = rule.Name
                });
            }
        }

        /// <inheritdoc />
        public async Task DeleteRuleAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
