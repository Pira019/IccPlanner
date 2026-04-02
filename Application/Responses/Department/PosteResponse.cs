namespace Application.Responses.Department
{
    public class PosteResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? ShortName { get; set; }
    }
}
