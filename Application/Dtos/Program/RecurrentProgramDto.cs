using Domain.Entities;

namespace Application.Dtos.Program
{
    public class RecurrentProgramDto
    {
        public int Id { get; set; }
        public int? DaysAhead { get; set; }
        public DateOnly? DateEnd { get; set; }
        public List<RecurrentPrgDepartmentInfoDto> PrgDepartmentInfos { get; set; } = [];
    }

    public class RecurrentPrgDepartmentInfoDto
    {
        public int Id { get; set; }
        public string? Day { get; set; }
        public List<DateOnly> ExistingDates { get; set; } = [];
        public List<ServiceTemplateDto> ServiceTemplates { get; set; } = [];
    }

    public class ServiceTemplateDto
    {
        public int TabServicesId { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public TimeOnly? ArrivalTimeOfMember { get; set; }
        public string? Days { get; set; }
        public string? Notes { get; set; }
    }
}
