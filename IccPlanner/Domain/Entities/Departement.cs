namespace IccPlanner.Domain.Entities
{
    public class Departement
    {
        private Guid Id {  get; set; }
        private string Name { get; set; }
        private string Description { get; set; }
        private  string shortName { get; set; }
        private DateOnly startDate { get; set; } // Date d'ouverture
        private Ministry Ministry { get; set; }
    }
}
