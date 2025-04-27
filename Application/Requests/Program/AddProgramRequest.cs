using System.ComponentModel.DataAnnotations;

namespace Application.Requests.Program
{
    /// <summary>
    /// Model de donnée pour ajouter un programme
    /// </summary>
    public class AddProgramRequest
    { 
        public required string Name { get; set; }
        /// <summary>
        /// Abréviation ou nom court (ex: APP)
        /// </summary>
        public string? ShortName { get; set; }
        public required string Description { get; set; } 
    }
}
