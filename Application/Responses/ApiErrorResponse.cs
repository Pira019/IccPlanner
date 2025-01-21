using System.Text.Json.Serialization;

namespace Application.Responses
{
    /// <summary>
    ///   Model Erreur Api
    /// </summary>
    public class ApiErrorResponse
    {
        /// <summary>
        ///   Status de la requette
        /// </summary>
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        /// <summary>
        ///   Code d'erreur
        /// </summary>
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("statusDescription")]
        public string StatusDescription { get; set; }

        /// <summary>
        ///   Message d'erreur
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; }

        /// <summary>
        ///   Liste de messages de validation
        /// </summary>
        [JsonPropertyName("validationErrors")]
        public string[] ValidationErrors { get; set; }
    }
}
