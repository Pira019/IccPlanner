using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Poste
{
    public class AddPosteRequest
    {
        [Required, MaxLength(55)]
        public required string Name { get; set; }
        public required string Description { get; set; }
        [MaxLength(15)]
        public string? ShortName { get; set; }
        public bool IndGest { get; set; } = false;
    }
}
