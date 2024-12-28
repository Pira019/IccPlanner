using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Profile : BaseEntity
    {
        private int Id { get; set; }
        [MaxLength(255)]
        private string Name { get; set; }
        private string Description { get; set; }    
    }
}
