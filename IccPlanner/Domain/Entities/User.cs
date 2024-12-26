namespace IccPlanner.Domain.Entities
{
    /// <summary>
    /// Table utilisateur qui permet d'indentifier un utilisateur
    /// </summary>
    public class User
    {
        private Guid Id { get; set; }
        private string password { get; set; }
        private Member Member { get; set; }
    }
}
