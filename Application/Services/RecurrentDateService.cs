using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
    public class RecurrentDateService : IRecurrentDateService
    {
        private readonly IDepartmentProgramRepository _departmentProgramRepository;

        public RecurrentDateService(IDepartmentProgramRepository departmentProgramRepository)
        {
            _departmentProgramRepository = departmentProgramRepository;
        }

        public async Task<int> GenerateRecurrentDatesAsync(int defaultDaysAhead)
        {
            // Récupérer les programmes récurrents actifs avec les infos nécessaires
            var recurrentPrograms = await _departmentProgramRepository.GetRecurrentProgramsForDateGenerationAsync();

            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            var totalCreated = 0;

            foreach (var program in recurrentPrograms)
            {
                var daysAhead = program.DaysAhead ?? defaultDaysAhead;
                var maxDate = today.AddDays(daysAhead);

                // Respecter la date de fin du programme si elle existe
                if (program.DateEnd.HasValue && program.DateEnd.Value < maxDate)
                {
                    maxDate = program.DateEnd.Value;
                }

                foreach (var pdi in program.PrgDepartmentInfos)
                {
                    if (string.IsNullOrEmpty(pdi.Day) || !int.TryParse(pdi.Day, out var dayOfWeek))
                    {
                        continue;
                    }

                    // Générer les dates manquantes
                    var newDates = new List<PrgDate>();
                    var current = today;

                    while (current <= maxDate)
                    {
                        if ((int)current.DayOfWeek == dayOfWeek && !pdi.ExistingDates.Contains(current))
                        {
                            newDates.Add(new PrgDate
                            {
                                Date = current,
                                PrgDepartmentInfoId = pdi.Id
                            });
                        }

                        current = current.AddDays(1);
                    }

                    if (newDates.Any())
                    {
                        // Sauvegarder les dates et copier les services
                        var created = await _departmentProgramRepository.CreateRecurrentDatesWithServicesAsync(
                            newDates, pdi.ServiceTemplates);
                        totalCreated += created;
                    }
                }
            }

            return totalCreated;
        }
    }
}
