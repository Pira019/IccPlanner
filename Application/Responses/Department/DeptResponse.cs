namespace Application.Responses.Department
{
    /// <summary>
    ///     Modèle de retour d'un département.
    /// </summary>
    public class DeptResponse
    {
        public int Id { get; set; }
        public int? MinistryId { get; set; }
        public string? Name { get; set; } 
        public string? Description { get; set; } 
        public string? ShortName { get; set; }
        public DateOnly? StartDate { get; set; }
    }
}
