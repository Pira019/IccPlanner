using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Ministry
{
    /// <summary>
    /// Modèle pour ajour un <see cref="Ministry"/>
    /// </summary>
    public class AddMinistryRequest
    {
        [Required]
        [MaxLength(255)]
        public required string Name { get; set; }
        [Required]
        public required string Description { get; set; }
    }
}
