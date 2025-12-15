using System.Text.Json.Serialization;

namespace Application.Responses.Ministry
{
    /// <summary>
    /// Model obtenir la liste des ministères 
    /// </summary>
    public class GetMinistriesResponse
    {
        [JsonPropertyName("id")]
        public required int Id { get; set; }

        /// <summary>
        /// Nom du ministère 
        /// </summary>
        /// 
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        public required string Description { get; set; }
    }
}
