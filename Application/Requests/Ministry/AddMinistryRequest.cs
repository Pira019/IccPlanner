namespace Application.Requests.Ministry
{
    /// <summary>
    /// Modèle pour ajour un <see cref="Ministry"/>
    /// </summary>
    public class AddMinistryRequest
    { 
        public required string Name { get; set; } 
        public required string Description { get; set; }
    }
}
