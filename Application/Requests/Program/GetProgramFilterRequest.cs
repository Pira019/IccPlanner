namespace Application.Requests.Program
{
    public class GetProgramFilterRequest
    {
        public string[]? DepartmentIds { get; set; }
        public int? Mois { get; set; } 
        public int? Year { get; set; } 
    }
}
