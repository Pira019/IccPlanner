using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    /// <summary>
    /// Represente les roles d'un utilisateur
    /// </summary>
    public class Role : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(55)]
        public string Name { get; set; }
        public string Description { get; set; }    
        public List<User> Users { get; set; }
    }
}
