namespace Application.Responses.Department
{
    public class PosteResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ShortName { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool IndGest { get; set; }
        public bool IndSystem { get; set; }
    }
}
