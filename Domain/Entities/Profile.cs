using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Profile : BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }    
    }
}
