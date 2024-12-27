 
namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Classe ministere ex: TELECOMMUNICATION
    /// </summary>
    public class Ministry
    {
        private int Id { get;}
        private string Name { get; set; } 
        private string Description { get; set; }
        public ICollection<Departement> Departements { get; } = new List<Departement>();
    }
}
