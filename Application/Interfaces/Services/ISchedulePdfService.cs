using Application.Responses.Planning;

namespace Application.Interfaces.Services
{
    public interface ISchedulePdfService
    {
        byte[] GenerateSchedulePdf(List<MonthlyPlanningResponse> data, string departmentName, string? departmentShortName, int month, int year);
        byte[] GenerateDailyPdf(List<MonthlyPlanningResponse> data, string departmentName, string? departmentShortName, DateOnly date);
    }
}
