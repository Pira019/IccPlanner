using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Permission
    {
        [MaxLength(255)]
        private string Name { get; set; }
        private string Description { get; set; }    
    }
}
